using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolsticeDataServices.DAL.Entities
{
    // The raw structure of a table and the related mapper class is "DeviceMapper.cs"
    public class Device
    {
        public long Id { get; set; }
        public string ConfigureResponseID { get; set; }
        public string Action { get; set; }
        public string DeviceId { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string LogicalDeviceId { get; set; }
        public bool IsLocked { get; set; }
        public string RegionId { get; set; }

        public string DeviceAccessKey { get; set; }
        public Guid LinkedAccessKey { get; set; }

        public bool IsDefault { get; set; }
        public virtual Region Regions { get; set; }



    }
}
