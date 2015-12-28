using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolsticeDataServices.DAL.Entities
{
    // The raw structure of a table and the related mapper class is "RegionMapper.cs"
    public class Region
    {
        //public int Id { get; set; }
        [Key]
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
