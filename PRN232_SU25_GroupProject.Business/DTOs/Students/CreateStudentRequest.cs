using System.ComponentModel.DataAnnotations;

namespace PRN232_SU25_GroupProject.Business.DTOs.Students
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
