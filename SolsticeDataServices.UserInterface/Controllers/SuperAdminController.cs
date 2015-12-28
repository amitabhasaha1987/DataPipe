using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolsticeDataServices.DAL.Implementation;
using SolsticeDataServices.DAL.Entities;
using System.Threading.Tasks;
using SolsticeDataServices.Security;

namespace SolsticeDataServices.UserInterface.Controllers
{
    [CustomAuthorize(Roles = "SuperAdmin")]
    public class SuperAdminController : BaseController
    {
        // GET: SuperAdmin
        public async Task<ActionResult> Index()
        {
            Register register = new Register();
            IEnumerable<Users> signups = await register.GetSignUps();
            return View(signups);
        }

        public async Task<JsonResult> Accept(int UserId, string Email, string Password)
        {
            var register = new Register();
            var user = new Users { UserId = UserId, Password = Password, EmailId = Email};
            var status = await register.Acceptence(user);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Reject(int id)
        {
            Register register = new Register();
            Users user = new Users();
            user.UserId = id;
            bool isAccepted = await register.Rejection(user);
            JsonResult json = new JsonResult();
            json = Json(isAccepted, JsonRequestBehavior.AllowGet);
            return json;
        }
    }
}