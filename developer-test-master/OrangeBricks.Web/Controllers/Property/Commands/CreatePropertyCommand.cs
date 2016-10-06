using System.ComponentModel.DataAnnotations;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class CreatePropertyCommand
    {
        [Required]
        public string PropertyType { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int NumberOfBedrooms { get; set; }

        [Required]
        public string SellerUserId { get; set; }
    }
}