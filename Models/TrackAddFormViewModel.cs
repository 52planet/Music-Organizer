using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A5LB.Models
{
    public class TrackAddFormViewModel
    {
         
        public int Id { get; set; }
        public string Clerk { get; set; }
        [Display(Name = "Composers Names")]
        public string Composers { get; set; }
        [Display(Name = "Track Name")]
        public string Name { get; set; }
        [Display(Name = "Track Genre")]
        public SelectList  Genre { get; set; }
        [Display(Name = "Album Name")]
        public string AlbumName { get; set; }
        public int AlbumId { get; set; }
    }
}