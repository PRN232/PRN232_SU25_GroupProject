using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Reports
{
    public class IncidentReport
    {
        public string Period { get; set; }
        public int TotalIncidents { get; set; }
        public Dictionary<string, int> IncidentsByType { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> IncidentsBySeverity { get; set; } = new Dictionary<string, int>();
        public List<MedicalIncidentDto> Details { get; set; } = new List<MedicalIncidentDto>();
    }
}
