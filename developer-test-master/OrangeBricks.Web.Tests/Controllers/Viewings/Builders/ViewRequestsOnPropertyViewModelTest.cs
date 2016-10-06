using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Models;
using System.Collections.Generic;
using OrangeBricks.Web.Tests.Controllers.Property.Builders;
using System.Linq;
using OrangeBricks.Web.Controllers.Viewings.Builders;
using OrangeBricks.Web.Controllers.Viewings.ViewModels;

namespace OrangeBricks.Web.Tests.Controllers.Viewings.Builders
{
    public class ViewRequestsOnPropertyViewModelTest
    {
        private ViewRequestsOnPropertyViewModelBuilder _handler;
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
            _handler = new ViewRequestsOnPropertyViewModelBuilder(_context);
        }

        [Test]
        public void BuildShouldReturnViewRequestsOnPropertyViewModelBuilder()
        {
            // Arrange
            var property = new Models.Property
            {
                Id = 1,
                StreetName = "Smith Street",
                Description = "",
                SellerUserId = "321",
                IsListedForSale = true,
                Viewings = new List<Viewing>()
            };

            var viewing = new Models.Viewing
            {
                Id = 1,
                BuyerUserId = "123",
                Status = ViewStatus.Pending
            };

            property.Viewings.Add(viewing);

            var propertyList = new List<Models.Property> { property };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(propertyList.AsQueryable());

            _context.Properties.Returns(mockSet);
            _viewings.Find(1).Returns(viewing);

            // Act
            ViewRequestsOnPropertyViewModel viewRequestsOnPropertyViewModel = _handler.Build(1, "321");

            // Assert
            Assert.True(viewRequestsOnPropertyViewModel.PropertyId == 1);
        }
    }
}
