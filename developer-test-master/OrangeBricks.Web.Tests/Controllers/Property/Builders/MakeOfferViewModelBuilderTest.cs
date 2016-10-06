using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Models;
using System.Collections.Generic;
using System.Linq;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Controllers.Property.ViewModels;

namespace OrangeBricks.Web.Tests.Controllers.Property.Builders
{
    public class MakeOfferViewModelBuilderTest
    {
        private MakeOfferViewModelBuilder _handler;
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
            _handler = new MakeOfferViewModelBuilder(_context);
        }

        [Test]
        public void BuildShouldReturnMakeOfferViewModel()
        {
            // Arrange
            var property = new Models.Property
            {
                Id = 1,
                StreetName = "Smith Street",
                Description = "",
                SellerUserId = "321",
                IsListedForSale = true,
                Offers = new List<Offer>()
            };

            var offer = new Models.Offer
            {
                Id = 1,
                BuyerUserId = "123",
                Status = OfferStatus.Pending
            };

            property.Offers.Add(offer);

            var propertyList = new List<Models.Property> { property };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(propertyList.AsQueryable());

            _context.Properties.Returns(mockSet);
            _offers.Find(1).Returns(offer);

            // Act
            MakeOfferViewModel makeOfferViewModel = _handler.Build(1, "123");

            // Assert
            Assert.True(makeOfferViewModel.PropertyId == 1);
        }
    }
}
