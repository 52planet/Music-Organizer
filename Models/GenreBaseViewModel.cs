using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5LB.Models
{
    public class GenreBaseViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Genre Name")]
        public string Name { get; set; }
    }
}