using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.Business.DTOs.MedicalIncidents;
using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles;
using PRN232_SU25_GroupProject.Business.DTOs.Reports;
using PRN232_SU25_GroupProject.Business.DTOs.StudentMedications;
using PRN232_SU25_GroupProject.Business.DTOs.Students;
using PRN232_SU25_GroupProject.Business.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DashboardData> GetDashboardDataAsync(int? userId = null)
        {
            var today = DateTime.Today;
            var weekStart = today.AddDays(-(int)today.DayOfWeek);

            return new DashboardData
            {
                TotalStudents = await _unitOfWork.StudentRepository.Query().CountAsync(),
                TotalIncidentsToday = await _unitOfWork.MedicalIncidentRepository
                    .Query().CountAsync(i => i.IncidentDate.Date == today),
                TotalIncidentsThisWeek = await _unitOfWork.MedicalIncidentRepository
                    .Query().CountAsync(i => i.IncidentDate >= weekStart),
                PendingMedicationApprovals = await _unitOfWork.StudentMedicationRepository
                    .Query().CountAsync(m => !m.IsApproved),
                ActiveVaccinationCampaigns = await _unitOfWork.VaccinationCampaignRepository
                    .Query().CountAsync(c => c.ScheduledDate >= today),
                ActiveCheckupCampaigns = await _unitOfWork.HealthCheckupCampaignRepository
                    .Query().CountAsync(c => c.ScheduledDate >= today),
                LowStockMedications = await _unitOfWork.MedicationRepository
                    .Query().CountAsync(m => m.StockQuantity < 10),
                ExpiringMedications = await _unitOfWork.MedicationRepository
                    .Query().CountAsync(m => m.ExpiryDate <= today.AddDays(30)),

                RecentIncidents = await _unitOfWork.MedicalIncidentRepository.Query()
                    .Include(i => i.Student)
                    .OrderByDescending(i => i.IncidentDate)
                    .Take(5)
                    .Select(i => new RecentIncident
                    {
                        Id = i.Id,
                        StudentName = i.Student.FullName,
                        Type = i.Type.ToString(),
                        Severity = i.Severity.ToString(),
                        IncidentDate = i.IncidentDate
                    })
                    .ToListAsync(),

                UpcomingEvents = await GetUpcomingEventsAsync()
            };
        }

        private async Task<List<UpcomingEvent>> GetUpcomingEventsAsync()
        {
            var today = DateTime.Today;
            var events = new List<UpcomingEvent>();

            var vaccineCampaigns = await _unitOfWork.VaccinationCampaignRepository
                .Query().Where(c => c.ScheduledDate >= today).ToListAsync();

            events.AddRange(vaccineCampaigns.Select(c => new UpcomingEvent
            {
                Type = "Vaccination",
                Title = c.CampaignName,
                Date = c.ScheduledDate,
                Description = $"Tiêm chủng cho khối {c.TargetGrades}"
            }));

            var checkups = await _unitOfWork.HealthCheckupCampaignRepository
                .Query().Where(c => c.ScheduledDate >= today).ToListAsync();

            events.AddRange(checkups.Select(c => new UpcomingEvent
            {
                Type = "Checkup",
                Title = c.CampaignName,
                Date = c.ScheduledDate,
                Description = $"Khám sức khỏe khối {c.TargetGrades}"
            }));

            return events.OrderBy(e => e.Date).ToList();
        }

        public async Task<List<IncidentReport>> GetIncidentReportsAsync(DateTime from, DateTime to)
        {
            var incidents = await _unitOfWork.MedicalIncidentRepository.Query()
                .Where(i => i.IncidentDate >= from && i.IncidentDate <= to)
                .ToListAsync();

            return incidents
                .GroupBy(i => i.IncidentDate.Date)
                .Select(g => new IncidentReport
                {
                    Period = g.Key.ToString("dd/MM/yyyy"),
                    TotalIncidents = g.Count(),
                    IncidentsByType = g.GroupBy(i => i.Type.ToString())
                        .ToDictionary(t => t.Key, t => t.Count()),
                    IncidentsBySeverity = g.GroupBy(i => i.Severity.ToString())
                        .ToDictionary(s => s.Key, s => s.Count()),
                    Details = _mapper.Map<List<MedicalIncidentDto>>(g.ToList())
                })
                .ToList();
        }

        public async Task<List<VaccinationReport>> GetVaccinationReportsAsync(int campaignId)
        {
            var campaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(campaignId);
            if (campaign == null) return new List<VaccinationReport>();

            var targetGrades = campaign.TargetGrades.Split(',').Select(g => g.Trim()).ToList();

            var allStudents = await _unitOfWork.StudentRepository
                .Query()
                .Where(s => targetGrades.Contains(s.ClassName))
                .ToListAsync();

            var consents = await _unitOfWork.GetRepository<MedicalConsent>()
                .Query()
                .Where(c => c.CampaignId == campaignId && c.ConsentGiven)
                .ToListAsync();

            var records = await _unitOfWork.VaccinationRecordRepository
                .Query()
                .Where(r => r.CampaignId == campaignId)
                .ToListAsync();

            var studentIds = records.Select(r => r.StudentId).Distinct().ToList();
            var students = await _unitOfWork.StudentRepository.Query()
                .Where(s => studentIds.Contains(s.Id))
                .ToDictionaryAsync(s => s.Id);

            var mappedRecords = records.Select(r =>
            {
                var dto = _mapper.Map<VaccinationRecordDto>(r);
                if (students.TryGetValue(r.StudentId, out var student))
                {
                    dto.StudentName = student.FullName;
                    dto.StudentCode = student.StudentCode;
                }
                return dto;
            }).ToList();

            var report = new VaccinationReport
            {
                CampaignName = campaign.CampaignName,
                TotalEligible = allStudents.Count,
                ConsentReceived = consents.Count,
                VaccinationsCompleted = records.Count(r => r.Result == VaccinationResult.Completed),
                VaccinationsDeferred = records.Count(r => r.Result == VaccinationResult.Deferred),
                CompletionRate = allStudents.Count > 0
                    ? (double)records.Count(r => r.Result == VaccinationResult.Completed) * 100 / allStudents.Count
                    : 0,
                Details = mappedRecords
            };

            return new List<VaccinationReport> { report };
        }


        public async Task<List<HealthCheckupReport>> GetHealthCheckupReportsAsync(int campaignId)
        {
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(campaignId);
            if (campaign == null) return new List<HealthCheckupReport>();

            var results = await _unitOfWork.HealthCheckupResultRepository
                .Query()
                .Where(r => r.CampaignId == campaignId)
                .ToListAsync();

            var studentIds = results.Select(r => r.StudentId).Distinct().ToList();
            var students = await _unitOfWork.StudentRepository.Query()
                .Where(s => studentIds.Contains(s.Id))
                .ToDictionaryAsync(s => s.Id);

            var mappedResults = results.Select(r =>
            {
                var dto = _mapper.Map<HealthCheckupResultDto>(r);
                if (students.TryGetValue(r.StudentId, out var student))
                {
                    dto.StudentName = student.FullName;
                    dto.StudentCode = student.StudentCode;
                }
                return dto;
            }).ToList();

            var avgHeight = results.Any() ? results.Average(r => (double)r.Height) : 0;
            var avgWeight = results.Any() ? results.Average(r => (double)r.Weight) : 0;

            return new List<HealthCheckupReport>
    {
        new HealthCheckupReport
        {
            CampaignName = campaign.CampaignName,
            TotalStudents = results.Count,
            CheckupsCompleted = results.Count,
            RequiringFollowup = results.Count(r => r.RequiresFollowup),
            AverageMetrics = new Dictionary<string, double>
            {
                { "Height", avgHeight },
                { "Weight", avgWeight }
            },
            Details = mappedResults
        }
    };
        }



        public async Task<StudentHealthSummary> GetStudentHealthSummaryAsync(int studentId)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);

            var profile = await _unitOfWork.MedicalProfileRepository.Query()
                .Include(p => p.Allergies)
                .Include(p => p.ChronicDiseases)
                .Include(p => p.MedicalHistories)
                .FirstOrDefaultAsync(p => p.StudentId == studentId);

            var incidents = await _unitOfWork.MedicalIncidentRepository.Query()
                .Where(i => i.StudentId == studentId)
                .OrderByDescending(i => i.IncidentDate)
                .Take(5).ToListAsync();

            var vaccinations = await _unitOfWork.VaccinationRecordRepository.Query()
                .Where(r => r.StudentId == studentId)
                .ToListAsync();

            var checkups = await _unitOfWork.HealthCheckupResultRepository.Query()
                .Where(r => r.StudentId == studentId)
                .ToListAsync();

            var medications = await _unitOfWork.StudentMedicationRepository.Query()
                .Where(m => m.StudentId == studentId)
                .ToListAsync();

            return new StudentHealthSummary
            {
                Student = _mapper.Map<StudentDto>(student),
                MedicalProfile = _mapper.Map<MedicalProfileDto>(profile),
                RecentIncidents = _mapper.Map<List<MedicalIncidentDto>>(incidents),
                VaccinationHistory = _mapper.Map<List<VaccinationRecordDto>>(vaccinations),
                CheckupHistory = _mapper.Map<List<HealthCheckupResultDto>>(checkups),
                CurrentMedications = _mapper.Map<List<StudentMedicationDto>>(medications)
            };
        }
    }
}