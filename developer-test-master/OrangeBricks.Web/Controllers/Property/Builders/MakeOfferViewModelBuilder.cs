using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class MakeOfferViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public MakeOfferViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public MakeOfferViewModel Build(int id, string buyerId)
        {
            // Get the property
            var property = _context.Properties
                .Include(x => x.Offers).FirstOrDefault(x => x.Id == id);

            OfferStatus? lastOfferStatus = null;

            if (property != null)
            {
                // Has the property got any offers?
                if (property.Offers.Count > 0)
                {
                    // Get the last offer
                    var lastOrDefault = property.Offers.LastOrDefault(o => o.BuyerUserId == buyerId);
                    if (lastOrDefault != null)
                    {
                        lastOfferStatus = lastOrDefault.Status;
                    }
                }

                return new MakeOfferViewModel
                {
                    PropertyId = property.Id,
                    PropertyType = property.PropertyType,
                    StreetName = property.StreetName,
                    Offer = 100000, // TODO: property.SuggestedAskingPrice
                    Status = lastOfferStatus
                };
            }
            return new MakeOfferViewModel();
        }
    }
}