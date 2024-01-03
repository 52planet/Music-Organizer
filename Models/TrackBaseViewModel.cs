﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5LB.Models
{
    public class TrackBaseViewModel
    {
        public  int Id { get; set; }
        public string Clerk { get; set; }
        [Display(Name = "Composers names")]
        public string Composers { get; set; }
        [Display(Name = "Track Genre")]
        public string Genre { get; set; }
        [Display(Name = "Track Name")]
        public string Name { get; set; }
    }
}