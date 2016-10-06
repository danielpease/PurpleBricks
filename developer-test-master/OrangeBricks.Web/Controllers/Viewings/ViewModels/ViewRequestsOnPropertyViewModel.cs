using System.Collections.Generic;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewings.ViewModels
{
    public class ViewRequestsOnPropertyViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyType { get; set; }
        public int NumberOfBedrooms { get; set; }
        public string StreetName { get; set; }
        public bool HasViewings { get; set; }
        public ViewStatus Status { get; set; }
        public IEnumerable<ViewingViewModel> Viewings { get; set; }
    }
}