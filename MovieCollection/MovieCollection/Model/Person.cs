using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieCollection.Model
{
    public class Person
    {
        public int PersID { get; set; }

        [Required(ErrorMessage = "A firstname must be assigned.")]
        [StringLength(40, ErrorMessage = "The firstname can not have more than 40 characters.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "A lastname must be assigned.")]
        [StringLength(40, ErrorMessage = "The lastname can not have more than 40 characters.")]
        public string Lastname { get; set; }

        public string FullName { get; set; }
    }
}