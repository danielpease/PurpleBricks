using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class CreatePropertyCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public CreatePropertyCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(CreatePropertyCommand command)
        {
            // Create a new property
            var property = new Models.Property
            {
                PropertyType = command.PropertyType,
                StreetName = command.StreetName,
                Description = command.Description,
                NumberOfBedrooms = command.NumberOfBedrooms
            };

            property.SellerUserId = command.SellerUserId;

            // Add the property
            _context.Properties.Add(property);

            _context.SaveChanges();
        }
    }
}