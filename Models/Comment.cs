using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Travel_App.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        //many-to-one
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}