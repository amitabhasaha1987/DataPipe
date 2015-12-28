using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolsticeDataServices.UserInterface.Controllers
{
    public class FAQController : BaseController
    {
        // GET: FAQ
        public ActionResult Index()
        {
            return View();
        }
    }
}