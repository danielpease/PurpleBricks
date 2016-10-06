using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;


namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class MakeOfferCommandHandlerTest
    {
        private MakeOfferCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Models.Property> _properties;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _properties = Substitute.For<IDbSet<Models.Property>>();
            _context.Properties.Returns(_properties);
            _handler = new MakeOfferCommandHandler(_context);
        }

        [Test]
        public void HandleShouldMakeOffer()
        {
            // Arrange
            var command = new MakeOfferCommand
            {
                PropertyId = 1,
                Offer = 100000,
                BuyerUserId = "123"
            };

            var property = new Models.Property
            {
                Id = 1,
                Description = "Test Property",
                IsListedForSale = true
            };

            _properties.Find(1).Returns(property);

            // Act
            _handler.Handle(command);

            var thisProperty = _context.Properties.Find(command.PropertyId);

            // Assert
            Assert.That(thisProperty.Offers.Count == 1);
        }
    }
}
