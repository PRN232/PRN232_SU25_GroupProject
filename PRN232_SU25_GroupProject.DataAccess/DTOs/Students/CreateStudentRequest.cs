using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Students
{
    public class CreateStudentRequest
    {
        [Required]
        [MaxLength(20)]
        public string StudentCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string ClassName { get; set; }

        [Required]
        public int ParentId { get; set; }
    }
}
