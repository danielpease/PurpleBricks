using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;
using System;

namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class BookViewingCommandHandlerTest
    {
        private BookViewingCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Models.Property> _properties;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _properties = Substitute.For<IDbSet<Models.Property>>();
            _context.Properties.Returns(_properties);
            _handler = new BookViewingCommandHandler(_context);
        }

        [Test]
        public void HandleShouldAddViewing()
        {
            // Arrange
            var command = new BookViewingCommand {
                PropertyId = 1,
                ViewingDate = DateTime.Today
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
            Assert.That(thisProperty.Viewings.Count == 1);
        }
    }
}
