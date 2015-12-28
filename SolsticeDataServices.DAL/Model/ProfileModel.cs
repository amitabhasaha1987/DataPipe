using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolsticeDataServices.DAL.Entities;

namespace SolsticeDataServices.DAL.Model
{
    public class ProfileModel
    {
        public Users user;
        public Device device;
        public List<MasterDevice> allDevices;
        public List<Region> regions;
        public Device defaultDevice;
    }
}
