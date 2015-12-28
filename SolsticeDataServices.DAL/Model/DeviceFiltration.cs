using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolsticeDataServices.DAL.Model
{
    public class DeviceFiltration
    {
        public string LogicalDeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Region { get; set; }
        public bool IsDeviceActive { get; set; }
    }
}