using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using SolsticeDataServices.DAL.Context;
using System.Web.Security;
using System.Security.Principal;
using Newtonsoft.Json;
using SolsticeDataServices.Security;

namespace SolsticeDataServices.UserInterface
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {

                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                var newUser = new CustomPrincipal(authTicket.Name)
                {
                    UserId = serializeModel.UserId,
                    FirstName = serializeModel.FirstName,
                    LastName = serializeModel.LastName,
                    Roles = serializeModel.Roles,
                    LinkedAccessKey = serializeModel.LinkedAccessKey,
                    LastUsedLinkedAccessKey = serializeModel.LastUsedLinkedAccessKey,
                    Email = serializeModel.Email
                };

                HttpContext.Current.User = newUser;
            }

        }
    }
}
