using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_App.CustomValidation;



namespace Travel_App.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [MinLength(3, ErrorMessage = "Title cannot be less than 3!")]
        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Text cannot be less than 2!")]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        //many-to-many relationship

        public int CategoryId { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        //many-to-one relationship
        public virtual ICollection<Comment> Comments { get; set; }
        //one-to-one relationship
        public virtual Destination Destination { get; set; }


        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        // dropdown list
        public IEnumerable<SelectListItem> CommentList { get; set; }

        // checkboxes list
        [NotMapped]
        public List<CheckBoxViewModel> CategoriesList { get; set; }


    }
}