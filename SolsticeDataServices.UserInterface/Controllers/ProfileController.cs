using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolsticeDataServices.DAL.Context;
using System.Threading.Tasks;
using SolsticeDataServices.DAL.Entities;
using SolsticeDataServices.DAL.Implementation;
using SolsticeDataServices.DAL.Model;
using SolsticeDataServices.Security;

namespace SolsticeDataServices.UserInterface.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class ProfileController : BaseController
    {
        SolsticeDataServicesContext _context;
        ProfileModel myProfile;
        public ProfileController()
        {
            _context = new SolsticeDataServicesContext();
            myProfile = new ProfileModel()
            {
                device = new Device(),
                regions = new List<Region>(),
                allDevices = new List<MasterDevice>(),
                user = new Users(),
                defaultDevice = new Device()

            };
        }
        // GET: Profile
        public ActionResult Index()
        {

            myProfile.user = _context.Users.FirstOrDefault(m => m.LastUsedLinkedAccessKey == User.LastUsedLinkedAccessKey);

            GetDefaultDevice(myProfile);
            GetAllDevice();
            GetRegion();

            return View(myProfile);
        }

        public void GetDefaultDevice(ProfileModel myProfile)
        {
            myProfile.defaultDevice = _context.Devices.FirstOrDefault(d => d.LinkedAccessKey == User.LastUsedLinkedAccessKey && d.IsDefault == true);
        }

        public void GetAllDevice()
        {
            var RawDevice = _context.Devices.Select(d => d);

            //   var Dev=_context.Devices.Select(s=>s).Where()

            //var RawDevice = _context.Devices.Join(_context.UsersDevice, d => d.UsersDeviceMapId, u => u.UsersDeviceMapId, (d, u) => new
            //{
            //    UserDevaiceMapId = u.UsersDeviceMapId,
            //    DeviceID = d.DeviceId,
            //    Region = d.RegionId,
            //}).Join(_context.Users, m => m.UserDevaiceMapId, n => n.UsersDeviceMapId, (m, n) => new
            //{
            //    UserId = n.EmailId,
            //    DeviceID = m.DeviceID,
            //    Region = m.Region
            //}).Where(x => x.UserId == User.Identity.Name).Join(_context.MstrDevice, a => a.DeviceID, b => b.DeviceId, (a, b) => new
            //{
            //    DeviceId = a.DeviceID,
            //    DeviceName = b.DeviceName,
            //    Region = a.Region,
            //    IsLockActive = b.IsLockActive
            //}).ToList();

            ViewBag.Device = RawDevice;
        }

        public void GetRegion()
        {
            List<Region> region = _context.Region.ToList<Region>();
            myProfile.regions = region;
            ViewBag.Region = myProfile.regions;
        }

        [HttpGet]
        public async Task<JsonResult> LinkedAccessKey(string Email)
        {
            Users user = new Users();
            user.EmailId = Email;

            Profile profile = new Profile();
            Users loginUser = await profile.GetLinkedAccessKey(user);

            return Json(loginUser, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> ResetPassword(string Email, string Password)
        {
            Users user = new Users();
            user.EmailId = Email;
            user.Password = Password;

            DAL.Implementation.Profile profile = new Profile();

            bool IsPasswordReset = await profile.ResetPassword(user);
            return Json(new { IsPasswordReset = IsPasswordReset }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> SetDefault(string DeviceSerial)
        {
            DAL.Implementation.Profile profile = new Profile();
            Device DefaultDevice = await profile.SetDefault(DeviceSerial);
            return Json(new { Region = DefaultDevice.RegionId, DeviceAccess = DefaultDevice.DeviceAccessKey }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<JsonResult> UpdateDefaultDevice(string DeviceAccess, string Email, string DeviceSerial, string Region)
        {
            //Users user = new Users();
            //user.EmailId = Email;
            // user.LinkedAccessKey = DeviceSerial;//DBChange Logic is wrong here
            // user.RegionId = Region;

            ProfileModel updatedProfileModel = new ProfileModel();

            Device defaultDevice = new Device();
            Users user = new Users();

            defaultDevice.DeviceSerialNumber = DeviceSerial;
            user.UserId = User.UserId;
            user.LastUsedLinkedAccessKey = User.LastUsedLinkedAccessKey;

            updatedProfileModel.defaultDevice = defaultDevice;
            updatedProfileModel.user = user;


            DAL.Implementation.Profile profile = new Profile();
            bool IsUpdated = await profile.UpdateDefaultDevice(updatedProfileModel);
            return Json(new { IsUpdated = IsUpdated }, JsonRequestBehavior.AllowGet);
        }
    }
}
