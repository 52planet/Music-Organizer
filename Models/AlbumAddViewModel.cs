using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5LB.Models
{
    public class AlbumAddViewModel
    {
        public int Id { get; set; }
        public string Coordinator { get; set; }
        [Display(Name = "Album Name")]
        public string Name { get; set; }
        [Display(Name = "Album Release Date")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Album Cover")]
        public string UrlAlbum { get; set; }
        [Display(Name = "Album Genre")]
        public string Genre { get; set; }
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }

        public IEnumerable<int> ArtistIds { get; set; }
        public IEnumerable<int> TrackIds { get; set; }
    }
}