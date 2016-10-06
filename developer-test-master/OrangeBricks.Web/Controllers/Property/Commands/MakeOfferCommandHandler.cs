using System;
using System.Collections.Generic;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class MakeOfferCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public MakeOfferCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(MakeOfferCommand command)
        {
            // Get the property
            var property = _context.Properties.Find(command.PropertyId);

            if (property != null)
            {
                // Create a new offer
                var offer = new Offer
                {
                    Amount = command.Offer,
                    Status = OfferStatus.Pending,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    BuyerUserId = command.BuyerUserId
                };

                if (property.Offers == null)
                {
                    property.Offers = new List<Offer>();
                }

                // Add the offer
                property.Offers.Add(offer);

                _context.SaveChanges();
            }
        }
    }
}