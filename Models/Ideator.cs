using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Idea.Models
{
    public class Ideator
    {
        public int UserID { get; set; }

        public int IdeasID { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Discription of Idea")]
        public string Description { get; set; }
    }
}