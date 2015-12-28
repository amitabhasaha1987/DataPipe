using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolsticeDataServices.DAL.Entities;
using SolsticeDataServices.DAL.Context;
using System.Data.Entity;

namespace SolsticeDataServices.DAL.Common
{
    public class Common
    {
        SolsticeDataServicesContext context = new SolsticeDataServicesContext();

        public async Task<IQueryable<MasterDevice>> GetAllDeviceAsync()
        {
            var GetDevices = await context.MstrDevice.ToListAsync();
            return GetDevices.AsQueryable();
        }

        public IQueryable<MasterDevice> GetAllDevice()
        {
            var GetDevices = context.MstrDevice.ToList();
            return GetDevices.AsQueryable();
        }

        public IQueryable<Region> GetAllRegion()
        {
            var GetRegions = context.Region.ToList();
            return GetRegions.AsQueryable();
        }

        public IQueryable<Device> GetAllDeviceRegion(string DeviceId, string Region)
        {
            var AllDeviceRegion = from d in context.Devices
                                  join r in context.Region on d.RegionId equals r.RegionId
                                  join m in context.MstrDevice on d.DeviceId equals m.DeviceId
                                  where d.DeviceId == DeviceId && d.RegionId == Region
                                  select new Device
                                  {
                                      LogicalDeviceId = d.LogicalDeviceId,
                                      DeviceId = m.DeviceId,
                                      RegionId = r.RegionName
                                  };
            return AllDeviceRegion.AsQueryable();
        }
    }
}
