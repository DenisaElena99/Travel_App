using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Travel_App.CustomValidation;

namespace Travel_App.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [RegularExpression(@"^[^0-9]$", ErrorMessage = "Non-digit characters ")]
        public string Text { get; set; }

        [DateValidation( ErrorMessage = "Impossible" )]
        public DateTime Date { get; set; }

        //many-to-one relationship
        
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}