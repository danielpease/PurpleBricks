using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OrangeBricks.Web.Tests.Controllers.Property.Builders
{

    [TestFixture]
    public class MyPropertiesViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        [Test]
        public void BuildShouldReturnMyProperties()
        {
            // Arrange
            var builder = new MyPropertiesViewModelBuilder(_context);

            var properties = new List<Models.Property>{
                new Models.Property{ StreetName = "Smith Street", Description = "", SellerUserId = "123", IsListedForSale = true },
                new Models.Property{ StreetName = "Jones Street", Description = "", SellerUserId = "321", IsListedForSale = true}
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            // Act
            var viewModel = builder.Build("123");

            // Assert
            Assert.That(viewModel.Properties.Count, Is.EqualTo(1));
        }
    }
}
