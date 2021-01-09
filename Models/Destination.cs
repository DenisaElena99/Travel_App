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
        public int DestinationId { get; set; }
        [Required]
        public int Name { get; set; }

        public string Description { get; set; }
        [Required]
        public virtual Article Article { get; set; }

    }
}