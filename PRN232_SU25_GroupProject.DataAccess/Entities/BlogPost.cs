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
    public class BlogPost
    {
        [Key]
        public int PostID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int AuthorID { get; set; }

        [Required]
        public PostType PostType { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        [MaxLength(255)]
        public string ThumbnailURL { get; set; }

        public bool IsPublished { get; set; } = false;
        public DateTime? PublishedDate { get; set; }
        public int ViewCount { get; set; } = 0;

        [Required]
        public int SchoolID { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        // Navigation Properties
        [ForeignKey("AuthorID")]
        public virtual User Author { get; set; }

        [ForeignKey("SchoolID")]
        public virtual School School { get; set; }
    }
}
