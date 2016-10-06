using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Viewings.Builders;
using OrangeBricks.Web.Controllers.Viewings.Commands;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewings
{
    public class ViewingsController : Controller
    {
        private readonly IOrangeBricksContext _context;

        public ViewingsController(IOrangeBricksContext context)
        {
            _context = context;
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult Viewings(int id)
        {
            var builder = new ViewRequestsOnPropertyViewModelBuilder(_context);
            var viewModel = builder.Build(id, User.Identity.GetUserId());

            return View(viewModel);
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        [HttpPost]
        public ActionResult Confirm(ConfirmViewingCommand command)
        {
            var handler = new ConfirmViewingCommandHandler(_context);

            handler.Handle(command);

            return RedirectToAction("Viewings", new { id = command.PropertyId });
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        [HttpPost]
        public ActionResult Unavailable(UnavailableCommand command)
        {
            var handler = new UnavailableCommandHandler(_context);

            handler.Handle(command);

            return RedirectToAction("Viewings", new { id = command.PropertyId });
        }

        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult MyViewings()
        {
            var builder = new MyViewingsViewModelBuilder(_context);
            var viewModel = builder.Build(User.Identity.GetUserId());

            return View(viewModel);
        }
    }
}