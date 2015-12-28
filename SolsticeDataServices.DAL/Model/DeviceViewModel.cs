using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolsticeDataServices.DAL.Model
{
    public class DeviceViewModel
    {
        public string LogicalDeviceId { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string DeviceName { get; set; }
        public string Region { get; set; }
    }
}
