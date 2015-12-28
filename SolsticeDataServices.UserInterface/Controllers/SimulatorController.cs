using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolsticeDataServices.DAL.Entities;
using SolsticeDataServices.DAL.Implementation;
using System.Threading.Tasks;
using SolsticeDataServices.DAL.Context;
using SolsticeDataServices.Security;

namespace SolsticeDataServices.UserInterface.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class SimulatorController : BaseController
    {
        SolsticeDataServicesContext _context;
        //SolsticeDataServicesContext _context;
        public SimulatorController()
        {
            _context = new SolsticeDataServicesContext();
        }
        // GET: Simulator
        public ActionResult Index()
        {
            Device dev = new Device();
            dev = _context.Devices.FirstOrDefault(d => d.LinkedAccessKey == User.LastUsedLinkedAccessKey);
            ViewData["temp"] = dev;
            GetRegion();
            return View(dev);
            
        }

        public void GetRegion()
        {
            List<Region> region = _context.Region.ToList<Region>();
            //myProfile.regions = region;
            ViewBag.Region = region;
        }

        //public void GetRegion()
        //{
        //    List<Region> region = _context.Region.ToList<Region>();
        //   // myProfile.regions = region;
        //    ViewBag.Region = region;
        //}
    }
}