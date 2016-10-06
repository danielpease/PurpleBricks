using OrangeBricks.Web.Controllers.Offers.ViewModels;
using System.Collections.Generic;
using System.Linq;
using OrangeBricks.Web.Controllers.Viewings.ViewModels;

namespace OrangeBricks.Web.Helpers
{
    public static class EnumerableHelper
    {
        public static int LastOfferItem(IEnumerable<OfferViewModel> items)
        {
            var lastOffer = items.LastOrDefault();
            if (lastOffer != null)
            {
                return lastOffer.Id;
            }
            return 0;
        }

        public static int LastViewingItem(IEnumerable<ViewingViewModel> items)
        {
            var lastViewing = items.LastOrDefault();
            if (lastViewing != null)
            {
                return lastViewing.Id;
            }
            return 0;
        }
    }
}