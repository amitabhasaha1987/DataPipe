using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolsticeDataServices.DAL.Entities
{
    public class MasterDevice
    {
      //  public int Id { get; set; }

        [Key]
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
