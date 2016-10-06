using System;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Offers.Commands
{
    public class AcceptOfferCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public AcceptOfferCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(AcceptOfferCommand command)
        {
            // Find this offer and update
            var offer = _context.Offers.Find(command.OfferId);

            // Ensure the user has not changed the hidden form field to accept a different offer
            // by ensuring this offer belongs to the property
            var property = _context.Properties.Include(p => p.Offers).FirstOrDefault(o => o.Offers.Any(i => i.Id == offer.Id));

            if (property != null && (property.Id == command.PropertyId && offer != null))
            {
                offer.UpdatedAt = DateTime.Now;
                offer.Status = OfferStatus.Accepted;

                _context.SaveChanges();
            }
        }
    }
}