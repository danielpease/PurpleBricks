using OrangeBricks.Web.Models;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Property.ViewModels;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class BookViewingViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public BookViewingViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public BookViewingViewModel Build(int id, string buyerId)
        {
            // Get the property with viewings
            var property = _context.Properties.Include(o => o.Offers).Include(v => v.Viewings).FirstOrDefault(x => x.Id == id);

            OfferStatus? lastOfferStatus = null;
            bool viewingBooked = false;
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

                // Has the property got any viewings?
                if (property.Viewings.Count > 0)
                {
                    // Get the last viewing
                    var lastOrDefault = property.Viewings.LastOrDefault(o => o.BuyerUserId == buyerId);
                    if (lastOrDefault != null)
                    {
                        if (lastOrDefault.Status == ViewStatus.Confirmed || lastOrDefault.Status == ViewStatus.Pending)
                            viewingBooked = true;
                    }
                }

                return new BookViewingViewModel
                {
                    PropertyId = property.Id,
                    PropertyType = property.PropertyType,
                    StreetName = property.StreetName,
                    NumberOfBedrooms = property.NumberOfBedrooms,
                    Status = lastOfferStatus,
                    ViewingBooked = viewingBooked
                };
            }
            return new BookViewingViewModel();
        }
    }
}