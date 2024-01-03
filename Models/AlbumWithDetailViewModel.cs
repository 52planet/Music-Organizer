using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5LB.Models
{
    public class AlbumWithDetailViewModel : AlbumBaseViewModel
    {
        public AlbumWithDetailViewModel()
        {
            Artists = new List<ArtistBaseViewModel>();
            Tracks = new List<TrackBaseViewModel>();
        }
        [Display(Name = "Artists On Album")]
        public IEnumerable<ArtistBaseViewModel> Artists { get; set; }
        [Display(Name = "Tracks On Album")]
        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}