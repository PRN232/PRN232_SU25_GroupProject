using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class ConsentForm
    {
        [Key]
        public int ConsentFormID { get; set; }

        [Required]
        public FormType FormType { get; set; }

        [Required]
        public int RelatedID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int ParentID { get; set; }

        public ConsentStatus ConsentStatus { get; set; } = ConsentStatus.Pending;
        public DateTime? ResponseDate { get; set; }

        [MaxLength(500)]
        public string ResponseNote { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("ParentID")]
        public virtual User Parent { get; set; }
    }

}
