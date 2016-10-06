using OrangeBricks.Web.Controllers.Offers.ViewModels;
using System.Collections.Generic;
using System.Linq;

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
    }
}