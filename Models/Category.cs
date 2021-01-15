using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Travel_App.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]$", ErrorMessage = "Only letters allowed!")]
        public string CategoryName { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}