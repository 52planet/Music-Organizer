using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5LB.EntityModels
{
    public class Track
    {
        public int Id { get; set; }
        public string Clerk { get; set; }
        public string Composers { get; set; }
        public string Genre { get; set; }
        public string Name { get; set; }
        //nav props
        public ICollection<Album> Albums { get; set; }
    }
}