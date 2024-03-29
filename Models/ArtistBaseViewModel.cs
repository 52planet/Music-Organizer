﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5LB.Models
{
	public class ArtistBaseViewModel
	{
        public int Id { get; set; }
        [Display(Name = "Artist Birth Name")]
        public string BirthName { get; set; }
        [Display(Name = "Artist Birth/Start Date")]
        public DateTime BirthOrStartDate { get; set; }
        public string Executive { get; set; }
        [Display(Name = "Artist Genre")]
        public string Genre { get; set; }
        [Display(Name = "Artist Name")]
        public string Name { get; set; }
        [Display(Name = "Artist Photo")]
        public string UrlArtist { get; set; }

        
    }
}