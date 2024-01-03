using F2021A5LB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5LB.EntityModels
{
    public class Artist
    {
        public Artist()
        {
            //set dateTime
            BirthOrStartDate = DateTime.Now;
            Albums = new List<Album>();
        }
        public int Id { get; set; }
        public string BirthName { get; set; }
        public DateTime BirthOrStartDate { get; set; }
        public string Executive { get; set; }
        public string Genre { get; set; }
        public string Name { get; set; }
        public string UrlArtist { get; set; }
        //nav props
        public ICollection<Album> Albums { get; set; }
    }
}