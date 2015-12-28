using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolsticeDataServices.UserInterface.Controllers
{
    public class CaseStudiesController : BaseController
    {
        // GET: CaseStudies
        public ActionResult Index()
        {
            return View();
        }
    }
}