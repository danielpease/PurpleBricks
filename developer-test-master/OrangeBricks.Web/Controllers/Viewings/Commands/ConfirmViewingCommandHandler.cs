using OrangeBricks.Web.Models;
using System.Data.Entity;
using System.Linq;

namespace OrangeBricks.Web.Controllers.Viewings.Commands
{
    public class ConfirmViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public ConfirmViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(ConfirmViewingCommand command)
        {
            // Find the viewing
            var viewing = _context.Viewings.Find(command.ViewingId);

            // Ensure the user has not changed the hidden form field to accept a different viewing
            // by ensuring this viewing belongs to the property
            var property = _context.Properties.Include(p => p.Viewings).FirstOrDefault(v => v.Viewings.Any(i => i.Id == viewing.Id));

            if (property != null && (property.Id == command.PropertyId && viewing != null))
            {
                // Update the viewing status
                viewing.Status = ViewStatus.Confirmed;

                _context.SaveChanges();
            }
        }
    }
}