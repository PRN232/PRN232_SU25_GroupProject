using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repositories;
using PRN232_SU25_GroupProject.DataAccess.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork; private readonly IMapper _mapper;
        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentRequest request)
        {
            var student = _mapper.Map<Student>(request);
            await _unitOfWork.StudentRepository.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();

            var studentWithRelations = await _unitOfWork.StudentRepository
                .Query()
                .Include(s => s.Parent)
                .Include(s => s.MedicalProfile)
                .FirstOrDefaultAsync(s => s.Id == student.Id);

            return _mapper.Map<StudentDto>(studentWithRelations);
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _unitOfWork.StudentRepository
                .Query()
                .Include(s => s.Parent)
                .Include(s => s.MedicalProfile)
                .FirstOrDefaultAsync(s => s.Id == id);

            return student != null ? _mapper.Map<StudentDto>(student) : null;
        }

        public async Task<List<StudentDto>> GetStudentsByClassAsync(string className)
        {
            var students = await _unitOfWork.StudentRepository
                .Query()
                .Include(s => s.Parent)
                .Include(s => s.MedicalProfile)
                .Where(s => s.ClassName == className)
                .ToListAsync();

            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<List<StudentDto>> GetStudentsByParentAsync(int parentId)
        {
            var students = await _unitOfWork.StudentRepository
                .Query()
                .Include(s => s.Parent)
                .Include(s => s.MedicalProfile)
                .Where(s => s.ParentId == parentId)
                .ToListAsync();

            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<bool> UpdateStudentAsync(UpdateStudentRequest request)
        {
            var student = await _unitOfWork.StudentRepository
                .Query()
                .Include(s => s.Parent)
                .Include(s => s.MedicalProfile)
                .FirstOrDefaultAsync(s => s.Id == request.Id);

            if (student == null)
                return false;

            _mapper.Map(request, student);
            _unitOfWork.StudentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}