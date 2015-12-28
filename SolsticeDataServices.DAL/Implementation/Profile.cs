using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolsticeDataServices.DAL.Context;
using SolsticeDataServices.DAL.Entities;
using System.Data.Entity;
using SolsticeDataServices.DAL.Model;

namespace SolsticeDataServices.DAL.Implementation
{
    public class Profile
    {
        SolsticeDataServicesContext _context;

        public Profile()
        {
            _context = new SolsticeDataServicesContext();
        }

        public async Task<Users> GetLinkedAccessKey(Users user)
        {
            Users myuser = new Users();
            myuser = await _context.Users.Where(u => u.EmailId == user.EmailId).FirstOrDefaultAsync();
            return myuser;
        }

        public async Task<bool> ResetPassword(Users user)
        {
            Users myuser = new Users();
            myuser = await _context.Users.Where(u => u.EmailId == user.EmailId).FirstOrDefaultAsync();
            myuser.Password = user.Password;
            int _affectedRows = await _context.SaveChangesAsync();

            if (_affectedRows > 0)
                return true;
            else
                return false;
        }

        public async Task<Device> SetDefault(string DefaultDeviceSlNo)
        {

            Device myDevice = new Device();
            myDevice = await _context.Devices.Where(d => d.DeviceSerialNumber == DefaultDeviceSlNo).FirstOrDefaultAsync();
            return myDevice;
        }

        public async Task<bool> UpdateDefaultDevice(ProfileModel updatedProfileModel)
        {
            //Users myuser = new Users();
            //myuser = await _context.Users.Where(u => u.EmailId == user.EmailId).FirstOrDefaultAsync();

            //myuser.UsersDevice.LogicalDeviceId = user.UsersDevice.LogicalDeviceId;//DBChange
            //myuser.RegionId = user.RegionId;

            //Device defaultDevice = new Device();

            updatedProfileModel.defaultDevice = await _context.Devices.FirstOrDefaultAsync(d => d.DeviceSerialNumber == updatedProfileModel.defaultDevice.DeviceSerialNumber && d.LinkedAccessKey == updatedProfileModel.user.LastUsedLinkedAccessKey);
            updatedProfileModel.defaultDevice.IsDefault = true;

            Device myDevice = new Device();
            myDevice = updatedProfileModel.defaultDevice;

            //defaultDevice.

            int _affectedRows = await _context.SaveChangesAsync();
            if (_affectedRows > 0)
                return true;
            else
                return false;

        }
    }
}
