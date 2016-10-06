using OrangeBricks.Web.Models;
using System.Data.Entity;
using System.Linq;

namespace OrangeBricks.Web.Controllers.Viewings.Commands
{
    public class UnavailableCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public UnavailableCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(UnavailableCommand command)
        {
            // Get the viewing
            var viewing = _context.Viewings.Find(command.ViewingId);

            // Ensure the user has not changed the hidden firm field to accept a different viewing
            // by ensuring this viewing belongs to the property
            var property = _context.Properties.Include(p => p.Viewings).FirstOrDefault(v => v.Viewings.Any(i => i.Id == viewing.Id));

            if (property != null && (property.Id == command.PropertyId && viewing != null))
            {
                // Update the viewing status
                viewing.Status = ViewStatus.Unavailable;

                _context.SaveChanges();
            }
        }
    }
}