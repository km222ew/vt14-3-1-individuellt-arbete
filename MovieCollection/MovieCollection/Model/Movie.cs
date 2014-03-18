using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieCollection.Model
{
    /// <summary>
    /// Klass för hantering av filmer.
    /// </summary>
    public class Movie
    {
        public int MovieID { get; set; }

        [Required(ErrorMessage = "A title must be assigned")]
        [StringLength(60, ErrorMessage = "The title can not have more than 60 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A story ´must be assigned")]
        [StringLength(1000, ErrorMessage = "The story can not have more than 1000 characters")]
        public string Story { get; set; }
    }
}