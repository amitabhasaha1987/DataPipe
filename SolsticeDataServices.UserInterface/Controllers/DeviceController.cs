using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolsticeDataServices.DAL.Common;
using SolsticeDataServices.DAL.Entities;
using SolsticeDataServices.DAL.Context;
using System.Threading.Tasks;
using SolsticeDataServices.DAL.Implementation;
using System.Web.Security;
using System.Net.Http;
using SolsticeDataServices.DAL.Model;
using SolsticeDataServices.Security;

namespace SolsticeDataServices.UserInterface.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class DeviceController : BaseController
    {
        SolsticeDataServicesContext _context;
        DeviceRepository deviceRepository;
        public DeviceController()
        {
            _context = new SolsticeDataServicesContext();
        }

        // GET: Device
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult FillGrid()
        {
            var devices = _context.Devices.Include("Regions").Where(m => m.LinkedAccessKey == User.LastUsedLinkedAccessKey)
                .Join(
                    _context.MstrDevice, d => d.DeviceId, m => m.DeviceId, (d, m) => new DeviceFiltration
                    {
                        LogicalDeviceId = d.LogicalDeviceId,
                        DeviceName = m.DeviceName,
                        Region = d.RegionId,
                        IsDeviceActive = d.IsLocked
                    }).ToList();

            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillDevice()
        {
            var rawDevices = _context.MstrDevice.ToList();
            return Json(rawDevices, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillRegion()
        {
            var region = _context.Region.ToList();
            return Json(region, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            GetDeviceDefinitionList();
            GetRegionList();

            return View();
        }

        public void GetDeviceDefinitionList()
        {
            Common common = new Common();
            IQueryable<MasterDevice> masterDevice = common.GetAllDevice();
            ViewBag.DeviceDefinitionId = masterDevice;
        }

        public void GetRegionList()
        {
            Common common = new Common();
            IQueryable<Region> Regions = common.GetAllRegion();
            ViewBag.Region = Regions;
        }

        [HttpPost]
        public async Task<ActionResult> AddDevice(Device device)
        {
            if (ModelState.IsValid)
            {
                string Type = "Device/AddLogicalDevice";
                string Params = "action=AddLogicalDevice&accesskey=" + User.LastUsedLinkedAccessKey + "&deviceserialnumber=" + device.DeviceSerialNumber +
                    "&logicaldeviceid=" + device.LogicalDeviceId + "&devicedefinitionid=" + device.DeviceId + "&region=" + device.RegionId;
                //+ "&email=" + User.Identity.Name;

                string response = Model.ResponseUtility.GetRespose(Type, Params);

                string[] responseMessage = response.Split(new string[] { "**" }, StringSplitOptions.None);
                if (responseMessage[1].Contains("Bad Request") && responseMessage[0].Contains("LogicalDeviceId"))
                {
                    string str = "<p>Error!&nbsp;&nbsp;&nbsp;&nbsp;Error adding the device " + device.LogicalDeviceId + "</p><p>" +
                          "https://solstice.tersosolutions.com/v1.2/application/?action=addlogicaldevice&amp;accesskey=" + device.DeviceAccessKey + "&amp;deviceserialnumber=" + device.DeviceSerialNumber +
                          "&amp;logicaldeviceid=" + device.LogicalDeviceId + "&amp;devicedefinitionid=" + device.DeviceId + "&amp;region=" + device.RegionId + "</p><p>";
                    //string[] errorType = responseMessage[0].Split(new string[] { "$$" }, StringSplitOptions.None);
                    //TempData["SubmitError"] = responseMessage[0].Substring(12).Replace("\"", "");
                    //"<p>Error!&nbsp;&nbsp;&nbsp;&nbsp;Error adding the device " + LogicalDeviceId + "</p><p>" +
                    //"https://solstice.tersosolutions.com/v1.2/application/?action=addlogicaldevice&amp;accesskey=" + AccessKey + "&amp;deviceserialnumber=" + DeviceSerialNumber +
                    // "&amp;logicaldeviceid=" + LogicalDeviceId + "&amp;devicedefinitionid=" + DeviceDefinitionId + "&amp;region=" + Region + "</p><p>" +
                    // "A Device with the attribute LogicalDeviceId equal to " + LogicalDeviceId + " already exists</p>"
                    TempData["SubmitErrorDtls"] = str;
                    TempData["SubmitError"] = responseMessage[0];
                    return RedirectToAction("Add");
                }
                else if (responseMessage[1].Contains("Bad Request") && responseMessage[0].Contains("DeviceSerialNumber"))
                {
                    string str = "<p>Error!&nbsp;&nbsp;&nbsp;&nbsp;Error adding the device " + device.LogicalDeviceId + "</p><p>" +
                         "https://solstice.tersosolutions.com/v1.2/application/?action=addlogicaldevice&amp;accesskey=" + device.DeviceAccessKey + "&amp;deviceserialnumber=" + device.DeviceSerialNumber +
                         "&amp;logicaldeviceid=" + device.LogicalDeviceId + "&amp;devicedefinitionid=" + device.DeviceId + "&amp;region=" + device.RegionId + "</p><p>";
                    TempData["SubmitErrorDtls"] = str;
                    TempData["SubmitError"] = responseMessage[0];
                    return RedirectToAction("Add");
                }
                else
                {
                    string str = "<p>Successfully added the device  " + device.LogicalDeviceId + "</p><p>" +
                        "https://solstice.tersosolutions.com/v1.2/application/?action=addlogicaldevice&amp;accesskey=" + device.DeviceAccessKey + "&amp;deviceserialnumber=" + device.DeviceSerialNumber +
                        "&amp;logicaldeviceid=" + device.LogicalDeviceId + "&amp;devicedefinitionid=" + device.DeviceId + "&amp;region=" + device.RegionId + "</p><p>";
                    TempData["SubmitSuccessDtls"] = str;
                    TempData["SubmitSuccess"] = responseMessage[0];

                    //TempData["SubmitSuccessDtls"] = str;
                    //TempData["SubmitSuccess"] = device.LogicalDeviceId;
                    return RedirectToAction("Index");
                }
            }
            else
                return View(device);
        }

        public async Task<JsonResult> DeviceDelete(string DeviceId)
        {
            deviceRepository = new DeviceRepository();
            bool IsDeleted = await deviceRepository.Delete(DeviceId);

            //if (IsDeleted)
            //    return Json(true);
            //else
            //    return Json(false);

            return Json(IsDeleted, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> Filter(string Device, string Region)
        {
            List<DeviceFiltration> devices;

            if (string.IsNullOrEmpty(Device) && string.IsNullOrEmpty(Region))
            {
                devices =
                    await _context.Devices.Include("Regions").Where(m => m.LinkedAccessKey == User.LastUsedLinkedAccessKey)
                        .Join(
                            _context.MstrDevice, d => d.DeviceId, m => m.DeviceId, (d, m) => new DeviceFiltration
                            {
                                LogicalDeviceId = d.LogicalDeviceId,
                                DeviceName = m.DeviceName,
                                Region = d.RegionId,
                                IsDeviceActive = d.IsLocked
                            }).ToListAsync();
            }
            else if (string.IsNullOrEmpty(Device) && !string.IsNullOrEmpty(Region))
            {
                devices =
                    await
                        _context.Devices.Include("Regions")
                            .Where(m => m.LinkedAccessKey == User.LastUsedLinkedAccessKey && m.RegionId == Region)
                            .Join(
                                _context.MstrDevice, d => d.DeviceId, m => m.DeviceId, (d, m) => new DeviceFiltration
                                {
                                    LogicalDeviceId = d.LogicalDeviceId,
                                    DeviceName = m.DeviceName,
                                    Region = d.RegionId,
                                    IsDeviceActive = d.IsLocked
                                }).ToListAsync();
            }
            else if (!string.IsNullOrEmpty(Device) && string.IsNullOrEmpty(Region))
            {
                devices =
                    await
                        _context.Devices.Include("Regions")
                            .Where(m => m.LinkedAccessKey == User.LastUsedLinkedAccessKey && m.DeviceId == Device)
                            .Join(
                                _context.MstrDevice, d => d.DeviceId, m => m.DeviceId, (d, m) => new DeviceFiltration
                                {
                                    LogicalDeviceId = d.LogicalDeviceId,
                                    DeviceName = m.DeviceName,
                                    Region = d.RegionId,
                                    IsDeviceActive = d.IsLocked
                                }).ToListAsync();
            }
            else
            {
                devices =
                    await
                        _context.Devices.Include("Regions")
                            .Where(
                                m =>
                                    m.LinkedAccessKey == User.LastUsedLinkedAccessKey && m.DeviceId == Device &&
                                    m.RegionId == Region)
                            .Join(
                                _context.MstrDevice, d => d.DeviceId, m => m.DeviceId, (d, m) => new DeviceFiltration
                                {
                                    LogicalDeviceId = d.LogicalDeviceId,
                                    DeviceName = m.DeviceName,
                                    Region = d.RegionId,
                                    IsDeviceActive = d.IsLocked
                                }).ToListAsync();

            }
            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDeviceInfo(string logicalDeviceId)
        {
            var device = await _context.Devices.Include("Regions").Where(m => m.LogicalDeviceId == logicalDeviceId).Join(
                        _context.MstrDevice, d => d.DeviceId, m => m.DeviceId, (d, m) => new DeviceViewModel
                        {
                            DeviceName = d.LogicalDeviceId,
                            DeviceSerialNumber = d.DeviceSerialNumber,
                            LogicalDeviceId = m.DeviceName,
                            Region = d.RegionId
                        }).FirstOrDefaultAsync();

            return Json(device, JsonRequestBehavior.AllowGet);
        }
    }
}
