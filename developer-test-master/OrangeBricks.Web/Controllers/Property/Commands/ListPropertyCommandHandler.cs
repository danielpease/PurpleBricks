using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class ListPropertyCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public ListPropertyCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(ListPropertyCommand command)
        {
            // Find the property and list it for sale
            var property = _context.Properties.Find(command.PropertyId);

            if (property != null)
            {
                property.IsListedForSale = true;
                _context.SaveChanges();
            }
        }
    }
}