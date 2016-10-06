using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Viewings.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewings.Builders
{
    public class ViewRequestsOnPropertyViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public ViewRequestsOnPropertyViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public ViewRequestsOnPropertyViewModel Build(int id, string sellerId)
        {
            // Get the property with any viewings
            var property = _context.Properties
                .Where(p => p.Id == id && p.SellerUserId == sellerId)
                .Include(x => x.Viewings)
                .SingleOrDefault();

            if (property != null)
            {
                var viewings = property.Viewings ?? new List<Viewing>();

                return new ViewRequestsOnPropertyViewModel
                {
                    HasViewings = viewings.Any(),
                    Viewings = viewings.Select(x => new ViewingViewModel
                    {
                        Id = x.Id,
                        ViewDate = x.ViewDate,
                        Status = x.Status.ToString(),
                        IsPending = x.Status == ViewStatus.Pending,
                    }),
                    PropertyId = property.Id,
                    PropertyType = property.PropertyType,
                    StreetName = property.StreetName,
                    NumberOfBedrooms = property.NumberOfBedrooms
                };
            }

            return new ViewRequestsOnPropertyViewModel();
        }
    }
}