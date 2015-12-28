using SolsticeDataServices.DAL.Context;
using SolsticeDataServices.UserInterface.Filter;
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
using SolsticeDataServices.Security;

//using SolsticeDataServices.DAL.Context;

namespace SolsticeDataServices.UserInterface.Controllers
{
    #if !DEBUG
    [WhiteSpaceFilter]
    #endif
    public class BaseController : Controller
    {
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}