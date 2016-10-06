using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public BookViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(BookViewingCommand command)
        {
            // Get the property
            var property = _context.Properties.Find(command.PropertyId);

            if (property != null)
            {
                var booking = new Viewing
                {
                    PropertyId = command.PropertyId,
                    ViewDate = command.ViewingDate,
                    BuyerUserId = command.BuyerUserId
                };

                if (property.Viewings == null)
                {
                    property.Viewings = new List<Viewing>();
                }

                // Add the viewing
                property.Viewings.Add(booking);

                _context.SaveChanges();
            }
        }
    }
}