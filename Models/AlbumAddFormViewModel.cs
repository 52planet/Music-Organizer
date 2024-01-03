using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A5LB.Models
{
    public class AlbumAddFormViewModel
    {
        public int Id { get; set; }
        public string Coordinator { get; set; }
        [Display(Name = "Album Name")]
        public string Name { get; set; }
        [Display(Name = "Album Realease Date")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Album Cover")]
        public string UrlAlbum { get; set; }
        [Display(Name = "Album Genre")]
        public SelectList GenreList { get; set; }
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }

        [Display(Name = "Artist List")]
        public MultiSelectList ArtistsList { get; set; }
        [Display(Name = "Track List")]
        public MultiSelectList TrackList { get; set; }
    }
}