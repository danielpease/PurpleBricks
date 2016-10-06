using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OrangeBricks.Web.Controllers.Viewings.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class BookViewingViewModel
    {
        public int PropertyId { get; set; }

        public string PropertyType { get; set; }

        public string StreetName { get; set; }

        public int NumberOfBedrooms { get; set; }

        [Required]
        [Display(Name = "Viewing date")]
        public DateTime ViewingDate { get; set; }

        public OfferStatus? Status { get; set; }

        public bool ViewingBooked { get; set; }

        public IEnumerable<ViewingViewModel> Viewings { get; set; }
    }
}