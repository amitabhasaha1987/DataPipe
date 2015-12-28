using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data.Entity;
using System.Threading.Tasks;
using SolsticeDataServices.DAL.Implementation;
using SolsticeDataServices.DAL.Entities;
using System.Web.Security;
using System.Net.Mail;
using Newtonsoft.Json;
using SolsticeDataServices.DAL.Context;
using SolsticeDataServices.Security;

namespace SolsticeDataServices.UserInterface.Controllers
{
    public class IndexController : BaseController
    {
        // GET: Index
        SolsticeDataServicesContext _context;
        public IndexController()
        {
            _context = new SolsticeDataServicesContext();
        }

        public ActionResult Index()
        {
            //string loginattempt = Convert.ToString(Request.QueryString["LoginAttempt"]);
            //if (loginattempt != null)
            //    TempData["UnsuccessfulAttempt"] = Convert.ToInt32(loginattempt);
            //else
            //    TempData["UnsuccessfulAttempt"] = 0;

            //var UserId = Convert.ToInt64(User.Identity.Name);

            //var UserEmailID = _context.Users.Where(x => x.UserId == UserId).Select(y => y.EmailId).ToList();
            //if (UserEmailID.Count() != 0)
            //{
            //    ViewBag.Welcome = Convert.ToString(UserEmailID[0]);
            //}

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Users u)
        {
            Login login = new Login();

            if (!String.IsNullOrEmpty(u.EmailId) && !String.IsNullOrEmpty(u.Password))
            {
                var user = new Users {EmailId = u.EmailId, Password = u.Password};

                var loginuser = await login.LoggingIn(user);
                if (loginuser != null)
                {
                    CustomPrincipalSerializeModel serializeModel;

                    if (loginuser.IsSuperAdmin)
                    {
                        
                             serializeModel = new CustomPrincipalSerializeModel
                            {
                                FirstName = loginuser.FirstName,
                                LastName =  loginuser.LastName,
                                UserId =  loginuser.UserId,
                                Email = loginuser.EmailId,
                                LinkedAccessKey =  loginuser.LinkedAccessKey,
                                LastUsedLinkedAccessKey = loginuser.LastUsedLinkedAccessKey,
                                Roles = new[] { "SuperAdmin", "User" },
                                
                            };
                    }
                    else
                    {

                         serializeModel = new CustomPrincipalSerializeModel
                        {
                            FirstName = loginuser.FirstName,
                            LastName = loginuser.LastName,
                            UserId = loginuser.UserId,
                            Email = loginuser.EmailId,
                            LinkedAccessKey = loginuser.LinkedAccessKey,
                            LastUsedLinkedAccessKey = loginuser.LastUsedLinkedAccessKey,
                            Roles = new[] { "User" }
                        };
                    }

                    var userData = JsonConvert.SerializeObject(serializeModel);

                    var authTicket = new FormsAuthenticationTicket(
                                           1,
                                           loginuser.EmailId,
                                           DateTime.Now,
                                           DateTime.Now.AddMinutes(15),
                                           false,
                                           userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);


                    if (loginuser.IsSuperAdmin)
                        return RedirectToAction("Index", "SuperAdmin");
                    else
                        return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    int UnsuccessfulAttempt = Convert.ToInt32(TempData["UnsuccessfulAttempt"]) + 1;
                    TempData["UnsuccessfulAttempt"] = UnsuccessfulAttempt;
                    return Redirect("/Index?LoginAttempt=" + UnsuccessfulAttempt.ToString());
                }
            }
            else
                return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult AcceptSignUps()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        public async Task<ActionResult> ForgetPassword(Users user)
        {
            DAL.Implementation.Login login = new Login();
            Users myuser = new Users();
            myuser = await login.IsUserRegistered(user);

            if (myuser != null)
            {
                //mail will go through this section
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("ResetPassword");
        }
    }
}
