using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrangeBricks.Web.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string PropertyType { get; set; }

        [Required]
        [MaxLength(50)]
        public string StreetName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public int NumberOfBedrooms { get; set; }

        [Required]
        public string SellerUserId { get; set; }

        public bool IsListedForSale { get; set; }

        public ICollection<Offer> Offers { get; set; }

        public ICollection<Viewing> Viewings { get; set; }
    }
}