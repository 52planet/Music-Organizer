using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5LB.EntityModels
{
    public class Album
    {
        public Album()
        {
            //set date time
            ReleaseDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Genre { get; set; }
        public string Coordinator { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        [StringLength(500)]
        public string UrlAlbum { get; set; }
        //nav props
        public ICollection<Artist> Artists { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}