using System.ComponentModel.DataAnnotations;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class MakeOfferViewModel
    {
        public int PropertyId { get; set; }

        public string PropertyType { get; set; }

        public string StreetName { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Offer must be numeric")]
        public int Offer { get; set; }

        public OfferStatus? Status { get; set; }
    }
}