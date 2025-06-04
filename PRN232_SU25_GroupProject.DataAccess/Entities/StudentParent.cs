using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class StudentParent
    {
        [Key]
        public int StudentParentID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int ParentID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Relationship { get; set; }

        public bool IsPrimary { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("ParentID")]
        public virtual User Parent { get; set; }
    }
}
