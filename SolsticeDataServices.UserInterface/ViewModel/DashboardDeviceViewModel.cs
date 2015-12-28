using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolsticeDataServices.UserInterface.ViewModel
{
    public class DashboardDeviceViewModel
    {

        //public DashboardDeviceViewModel(List<DeviceViewModel> lstDev)
        //{
        //    lstDevice = lstDev;
        //}
        public List<DeviceViewModel> lstDevice { get; set; }

        public List<RegionCount> lstRegionCount { get; set; }

    }

    public class RegionCount
    {
        public string Region { get; set; }
        public int Cnt { get; set; }
    }
    public class DeviceViewModel
    {

        public string Region { get; set; }
        public int TotalCnt { get; set; }
        public string DeviceName { get; set; }

        public string DeviceDefinitionId { get; set; }
        //public int TotalSum { get; set; }

        //  public int Rgn { get; set; }

    }
}