using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Viewings.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewings.Builders
{
    public class MyViewingsViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public MyViewingsViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public MyViewingsViewModel Build(string buyerId)
        {
            // Get all properties where the buyer has booked a viewing
            var properties = _context.Properties
                .Include(x => x.Viewings)
                .Where(x => x.Viewings.Any(v => v.BuyerUserId == buyerId))
                .ToList();

            if (properties != null)
            {
                List<ViewRequestsOnPropertyViewModel> myViewings = properties.Select(property => new ViewRequestsOnPropertyViewModel
                {
                    PropertyId = property.Id,
                    PropertyType = property.PropertyType,
                    NumberOfBedrooms = property.NumberOfBedrooms,
                    StreetName = property.StreetName,
                    Viewings = property.Viewings.Select(x => new ViewingViewModel
                    {
                        Id = x.Id,
                        ViewDate = x.ViewDate,
                        Status = x.Status.ToString()
                    })
                }).ToList();

                return new MyViewingsViewModel
                {
                    Viewings = myViewings
                };
            }
            return new MyViewingsViewModel();
        }
    }
}