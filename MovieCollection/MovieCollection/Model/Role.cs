using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieCollection.Model
{
    public class Role
    {
        public int MovieID { get; set; }
        public int RoleID { get; set; }
        public int PersID { get; set; }

        [Required(ErrorMessage = "A role must be assigned.")]
        [StringLength(60, ErrorMessage = "The role can not have more than 60 characters.")]
        public string Role { get; set; }
    }
}