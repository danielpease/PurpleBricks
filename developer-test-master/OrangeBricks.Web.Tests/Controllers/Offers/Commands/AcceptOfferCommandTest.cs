using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Controllers.Offers.Commands;
using System.Collections.Generic;
using System.Linq;
using OrangeBricks.Web.Tests.Controllers.Property.Builders;

namespace OrangeBricks.Web.Tests.Controllers.Offers.Commands
{
    [TestFixture]
    public class AcceptOfferCommandTest
    {
        private AcceptOfferCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Models.Property> _properties;
        private IDbSet<Models.Offer> _offers;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _properties = Substitute.For<IDbSet<Models.Property>>();
            _context.Properties.Returns(_properties);
            _offers = Substitute.For<IDbSet<Models.Offer>>();
            _context.Offers.Returns(_offers);
            _handler = new AcceptOfferCommandHandler(_context);
        }

        [Test]
        public void HandleShouldUpdateToAcceptOffer()
        {
            // Arrange
            var command = new AcceptOfferCommand
            {
                OfferId = 1,
                PropertyId = 1
            };

            var property = new Models.Property
            {
                Id = 1,
                StreetName = "Smith Street",
                Description = "",
                IsListedForSale = true,
                Offers = new List<Offer>()
            };


            var offer = new Models.Offer
            {
                Id = 1,
                Amount = 100000,
                Status = OfferStatus.Pending
            };

            property.Offers.Add(offer);

            var propertyList = new List<Models.Property> {property};

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(propertyList.AsQueryable());

            _context.Properties.Returns(mockSet);
            _offers.Find(1).Returns(offer);

            // Act
            _handler.Handle(command);
            var updatedProperty = _context.Properties.FirstOrDefault();
            var updatedOffer = updatedProperty.Offers.FirstOrDefault(o => o.Id == 1);

            // Assert
            _context.Offers.Received(1).Find(1);
            _context.Received(1).SaveChanges();
            Assert.True(updatedOffer.Status == OfferStatus.Accepted);
        }
    }
}
