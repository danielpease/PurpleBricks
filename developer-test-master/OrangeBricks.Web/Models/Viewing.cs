using System;
using System.ComponentModel.DataAnnotations;

namespace OrangeBricks.Web.Models
{
    public class Viewing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public ViewStatus Status { get; set; }

        [Required]
        public DateTime ViewDate { get; set; }

        [Required]
        public string BuyerUserId { get; set; }

        
    }
}