using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolsticeDataServices.DAL.Entities;
using System.Data.Entity;
using System.Threading.Tasks;
using SolsticeDataServices.DAL.Implementation;

namespace SolsticeDataServices.UserInterface.Controllers
{
    public class SignUpController : BaseController
    {
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UserRegister(Users sign)
        {
            Register register = new Register();
            bool hasInserted = await register.UserSignUp(sign);
            if (hasInserted)
                return RedirectToAction("Index", "Index");
            else
                return RedirectToAction("Index");
        }
    }
}