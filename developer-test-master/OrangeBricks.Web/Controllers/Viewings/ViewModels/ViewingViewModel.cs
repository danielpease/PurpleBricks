using System;

namespace OrangeBricks.Web.Controllers.Viewings.ViewModels
{
    public class ViewingViewModel
    {
        public int Id;
        public DateTime ViewDate { get; set; }
        public string Status { get; set; }
        public bool IsPending { get; set; }
    }
}