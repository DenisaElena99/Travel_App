using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Travel_App.Models
{
    public class Destination
    {
        [Key]
        public int ArticleId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Name cannot be less than 1!")]
        public string Name { get; set; }
        [MinLength(2, ErrorMessage = "Name cannot be less than 15!")]
        public string Description { get; set; }
        [Required]
        public virtual Article Article { get; set; }

    }
}