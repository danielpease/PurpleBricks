using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Controllers.Offers.Commands;
using System.Collections.Generic;
using OrangeBricks.Web.Controllers.Viewings.Commands;
using OrangeBricks.Web.Tests.Controllers.Property.Builders;
using System.Linq;

namespace OrangeBricks.Web.Tests.Controllers.Viewings.Commands
{
    [TestFixture]
    public class ConfirmViewingCommandTest
    {
        private ConfirmViewingCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Models.Property> _properties;
        private IDbSet<Models.Viewing> _viewings;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _properties = Substitute.For<IDbSet<Models.Property>>();
            _context.Properties.Returns(_properties);
            _viewings = Substitute.For<IDbSet<Models.Viewing>>();
            _context.Viewings.Returns(_viewings);
            _handler = new ConfirmViewingCommandHandler(_context);
        }

        [Test]
        public void HandleShouldUpdateToConfirmViewing()
        {
            // Arrange
            var command = new ConfirmViewingCommand
            {
                ViewingId = 1,
                PropertyId = 1
            };

            var property = new Models.Property
            {
                Id = 1,
                StreetName = "Smith Street",
                Description = "",
                IsListedForSale = true,
                Viewings = new List<Viewing>()
            };

            var viewing = new Models.Viewing
            {
                Id = 1,
                Status = ViewStatus.Pending
            };

            property.Viewings.Add(viewing);

            var propertyList = new List<Models.Property> { property };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(propertyList.AsQueryable());

            _context.Properties.Returns(mockSet);
            _viewings.Find(1).Returns(viewing);

            // Act
            _handler.Handle(command);

            // Assert
            _context.Viewings.Received(1).Find(1);
            _context.Received(1).SaveChanges();
            Assert.True(viewing.Status == ViewStatus.Confirmed);
        }
    }
}
