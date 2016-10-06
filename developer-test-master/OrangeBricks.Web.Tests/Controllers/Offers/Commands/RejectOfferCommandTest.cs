using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Controllers.Offers.Commands;

namespace OrangeBricks.Web.Tests.Controllers.Offers.Commands
{
    [TestFixture]
    public class RejectOfferCommandTest
    {
        private RejectOfferCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Models.Offer> _offers;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _offers = Substitute.For<IDbSet<Models.Offer>>();
            _context.Offers.Returns(_offers);
            _handler = new RejectOfferCommandHandler(_context);
        }

        [Test]
        public void HandleShouldUpdateToRejectOffer()
        {
            // Arrange
            var command = new RejectOfferCommand
            {
                OfferId = 1
            };

            var offer = new Models.Offer
            {
                Id = 1,
                Amount = 100000,
                Status = OfferStatus.Pending
            };

            _offers.Find(1).Returns(offer);

            // Act
            _handler.Handle(command);

            // Assert
            _context.Offers.Received(1).Find(1);
            _context.Received(1).SaveChanges();
            Assert.True(offer.Status == OfferStatus.Rejected);
        }
    }
}
