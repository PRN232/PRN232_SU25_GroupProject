using System.ComponentModel.DataAnnotations;

namespace PRN232_SU25_GroupProject.Business.DTOs.Students
{
    public class UpdateStudentRequest
    {

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ClassName { get; set; }
        public int ParentId { get; set; }
    }
}
