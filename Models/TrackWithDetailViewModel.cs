﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5LB.Models
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        public TrackWithDetailViewModel()
        {
            Albums = new List<AlbumBaseViewModel>();
        }
        [Display(Name = "Album List")]
        public IEnumerable<AlbumBaseViewModel> Albums { get; set; }
        [Display(Name = "Album Names")]
        public IEnumerable<string> AlbumNames { get; set; }
    }
}