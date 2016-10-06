using System.Collections.Generic;
using System.Data.Entity;
using OrangeBricks.Web.Models;
using System.Linq;
using OrangeBricks.Web.Controllers.Offers.ViewModels;

namespace OrangeBricks.Web.Controllers.Offers.Builders
{
    public class MyOffersViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public MyOffersViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public MyOffersViewModel Build(string buyerId)
        {
            // Get all properties where the buyer has made an offer
            var properties = _context.Properties
                .Include(x => x.Offers)
                .Where(x => x.Offers.Any(o => o.BuyerUserId == buyerId))
                .ToList();

            List<OffersOnPropertyViewModel> allProperties = properties.Select(property => new OffersOnPropertyViewModel
            {
                PropertyId = property.Id,
                PropertyType = property.PropertyType,
                NumberOfBedrooms = property.NumberOfBedrooms,
                StreetName = property.StreetName,
                HasOffers = property.Offers.Any(),
                Offers = property.Offers.Select(x => new OfferViewModel
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    CreatedAt = x.CreatedAt,
                    Status = x.Status.ToString()
                }),
            }).ToList();

            return new MyOffersViewModel
            {
                Offers = allProperties
            };
        }
    }
}