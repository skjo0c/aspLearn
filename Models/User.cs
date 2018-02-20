using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Idea.Models
{
    public class User
    {
        //@firstname, @lastname, @username, @email, @password

        public int UserID { get; }

        [Required]
        [DisplayName("First Name")]
        public string firstname { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string lastname { get; set; }

        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [DisplayName("User Name")]
        public string username { get; set; }
        [Required]
        [DisplayName("Password")]
        [StringLength(10,ErrorMessage = "Must be at least 8 characters", MinimumLength = 7)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public Ideator Idea1 { get; set; }
        public List<Ideator> AllIdea { get; set; }
    }
}