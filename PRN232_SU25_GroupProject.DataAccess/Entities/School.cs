using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class School
    {
        [Key]
        public int SchoolID { get; set; }

        [Required]
        [MaxLength(100)]
        public string SchoolName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string PrincipalName { get; set; }

        [MaxLength(200)]
        public string HealthRoomInfo { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public virtual ICollection<Medicine> Medicines { get; set; } = new HashSet<Medicine>();
        public virtual ICollection<VaccinationCampaign> VaccinationCampaigns { get; set; } = new HashSet<VaccinationCampaign>();
        public virtual ICollection<HealthCheckupCampaign> HealthCheckupCampaigns { get; set; } = new HashSet<HealthCheckupCampaign>();
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new HashSet<BlogPost>();
    }
}
