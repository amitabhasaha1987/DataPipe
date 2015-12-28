using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolsticeDataServices.DAL.Entities;
using SolsticeDataServices.DAL.Context;
using SolsticeDataServices.DAL.Implementation;
using SolsticeDataServices.UserInterface.ViewModel;
using SolsticeDataServices.DAL.Model;
using SolsticeDataServices.Security;

namespace SolsticeDataServices.UserInterface.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class DashboardController : BaseController
    {
        SolsticeDataServicesContext _context;
        //DeviceRepository deviceRepository;

        public DashboardController()
        {
            _context = new SolsticeDataServicesContext();
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            var mstrDvcTemp = _context.Devices.Include("Regions").Where(m => m.LinkedAccessKey == User.LastUsedLinkedAccessKey)
                             .Join(
                             _context.MstrDevice, d => d.DeviceId, m => m.DeviceId, (d, m) => new
                             {
                                 LogicalDeviceId = d.LogicalDeviceId,
                                 DeviceName = m.DeviceName,
                                 Region = d.RegionId,
                                 IsDeviceActive = d.IsLocked,
                                 DeviceDefinitionId = d.DeviceId
                             }).ToList();

            var mstrDvc = mstrDvcTemp.GroupBy(d => new { d.DeviceName, d.DeviceDefinitionId, d.Region }).Select(dc => new ViewModel.DeviceViewModel
             {
                 TotalCnt = dc.Count(),
                 Region = dc.Key.Region,
                 DeviceName = dc.Key.DeviceName,
                 DeviceDefinitionId = dc.Key.DeviceDefinitionId
             }).ToList();


            DashboardDeviceViewModel DDVM = new DashboardDeviceViewModel();

            DDVM.lstDevice = mstrDvc;

            List<RegionCount> rgnCount = mstrDvcTemp.GroupBy(d => new { d.Region }).Select(dc => new RegionCount
            {
                Cnt = dc.Count(),
                Region = dc.Key.Region,
            }).ToList();


            DDVM.lstRegionCount = rgnCount;
            return View(DDVM);

        }
    }


}
