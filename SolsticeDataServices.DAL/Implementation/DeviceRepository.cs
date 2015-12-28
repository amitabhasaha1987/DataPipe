using SolsticeDataServices.DAL.Context;
using SolsticeDataServices.DAL.Entities;
using SolsticeDataServices.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SolsticeDataServices.DAL.Model;

namespace SolsticeDataServices.DAL.Implementation
{
    public class DeviceRepository : IRepository<Device>
    {
        private SolsticeDataServicesContext _context;
        public DeviceRepository()
        {
            _context = new SolsticeDataServicesContext();
        }

        // the "async" keyword asychronously call the Add method
        public async Task<Device> Add(Device itemtoadd)
        {
            try
            {
                // finding duplicate DeviceSerialNumber
                bool IsDeviceSerialNumberExist = _context.Devices.Any(d => d.DeviceSerialNumber == itemtoadd.DeviceSerialNumber && d.LinkedAccessKey == itemtoadd.LinkedAccessKey);
                // bool IsDeviceSerialNumberExist = _context.Devices.Any(d => d.DeviceSerialNumber == itemtoadd.DeviceSerialNumber && d.EmailId == itemtoadd.EmailId);//DBChange

                if (!IsDeviceSerialNumberExist)
                {
                    // finding duplicate LogicalDeviceId
                    bool IsLogicalDeviceIdExist = _context.Devices.Any(d => d.LogicalDeviceId == itemtoadd.LogicalDeviceId && d.LinkedAccessKey == itemtoadd.LinkedAccessKey);
                    //bool IsLogicalDeviceIdExist = _context.Devices.Any(d => d.LogicalDeviceId == itemtoadd.LogicalDeviceId && d.EmailId == itemtoadd.EmailId);//DBChange

                    // Insertion of duplicate DeviceSerialNumber & LogicalDeviceId is not allowed
                    if (!IsLogicalDeviceIdExist)
                    {
                        var users =
                            _context.Users.FirstOrDefault(
                                m => m.LinkedAccessKey == itemtoadd.LinkedAccessKey);
                        // ConfigureResponseId has been generated
                        itemtoadd.ConfigureResponseID = Guid.NewGuid().ToString();
                        // items are added to the context
                        users.LogicalDevices = new List<Device> {itemtoadd};
                        // items are saved in the database asynchronously and the "await" keyword do the trick

                        await _context.SaveChangesAsync();
                        return itemtoadd;
                    }
                    else
                    {
                        Device d = new Device();
                        d.ConfigureResponseID = "LogicalDeviceId";
                        return d;
                    }
                }
                else
                {
                    Device d = new Device();
                    d.ConfigureResponseID = "DeviceSerialNumber";
                    return d;
                }
            }
            catch (Exception ex)
            {
                return new Device();
            }
        }

        public async Task<bool> Update(Device itemtoupdate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(string id)
        {
            Device GetDevice = await Task.FromResult(_context.Devices.AsEnumerable().Where(d => d.LogicalDeviceId == id).FirstOrDefault());
            Device device = _context.Devices.Remove(GetDevice);
            int affectedRows = await _context.SaveChangesAsync();
            if (affectedRows > 0)
                return true;
            else
                return false;
        }

        public async Task<IQueryable<Device>> GetAll()
        {
            // fetch all the devices added to the database asynchronously
            var GetDevices = await _context.Devices.ToListAsync();
            return GetDevices.AsQueryable<Device>();
        }

        public async Task<IQueryable<Device>> GetAllDeviceUserWise(Device device)
        {
            // fetch all the devices added to the database asynchronously
            //var GetDevices = await _context.Devices.Where(d => d.EmailId == device.EmailId).ToListAsync();
            var GetDevices = await _context.Devices.ToListAsync();//DBChange
            return GetDevices.AsQueryable<Device>();
        }

        public async Task<IEnumerable<DeviceFiltration>> GetFilteredData(Device device)
        {
            return null;
            //    if (device != null)
            //    {

            //        var GetDevices = await _context.Devices.Where(d => d.RegionId == device.RegionId && d.UsersDevice.UserId == device.UsersDevice.UserId).Join(    //DBChange
            //                                _context.MstrDevice, d => d.DeviceId, m => m.DeviceId, (d, m) => new
            //                                {
            //                                    LogicalDeviceId = d.LogicalDeviceId,
            //                                    DeviceName = m.DeviceName,
            //                                    Region = d.RegionId,
            //                                    IsDeviceActive = m.IsLockActive
            //                                }).ToListAsync();

            //        return GetDevices.Select(x => new DeviceFiltration
            //        {
            //            DeviceName = x.DeviceName,
            //            LogicalDeviceId = x.LogicalDeviceId,
            //            Region = x.Region,
            //            IsDeviceActive = x.IsDeviceActive
            //        }).ToList();
            //    }
            //    else
            //    {
            //        return  null;
            //    }

        }

        //public async Task<DeviceViewModel> GetDeviceInfo(Device device)
        //{
        //    var specificDevice = await
        //        _context.Devices.Include("Regions").Where(m => m.LinkedAccessKey == device.LinkedAccessKey && m.LogicalDeviceId == device.LogicalDeviceId)
        //          .FirstOrDefaultAsync();

        //    return specificDevice;
        //}
    }
}
