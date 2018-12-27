using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Foody.Data.Models.Publishing
{
    public class Article : BaseModel
    {
        public Article()
        {
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }
        public FoodyUser Author { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime? ApprovedOn { get; set; }

        public DateTime? RejectedOn { get; set; }

        public bool IsApproved { get; set; }

        public bool IsSentForApproval { get; set; }

        public bool IsRejected { get; set; }

        public string RejectedByUser { get; set; }

        public string RejectComment { get; set; }

        public string ImageLocation { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
