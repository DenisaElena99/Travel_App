using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Travel_App.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Date { get; set; }

        //many-to-many
        public virtual ICollection<Category> Category { get; set; }
        //one-to-many
        public virtual ICollection<Comment> Comments { get; set; }
        //one-to-one
        public virtual Destination Destination { get; set; }
    }
}