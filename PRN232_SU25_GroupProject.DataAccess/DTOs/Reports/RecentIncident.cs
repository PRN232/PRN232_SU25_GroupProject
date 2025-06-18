using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Reports
{
    public class RecentIncident
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Type { get; set; }
        public string Severity { get; set; }
        public DateTime IncidentDate { get; set; }
    }

}
