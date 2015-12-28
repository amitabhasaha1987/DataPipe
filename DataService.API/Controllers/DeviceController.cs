using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml.Serialization;
using DataService.API.Utility;
using SolsticeDataServices.DAL.Entities;
using SolsticeDataServices.DAL.Implementation;
using SolsticeDataServices.DAL.Model;
using SolsticeDataServices.DAL.Repository;

namespace DataService.API.Controllers
{
    public class DeviceController : ApiController
    {
        private IRepository<Device> _deviceRepo;

        public DeviceController()
        {
            _deviceRepo = new DeviceRepository();
        }

        [HttpGet]
        public async Task<IHttpActionResult> AddLogicalDevice(string Action, string AccessKey, string DeviceSerialNumber,
            string LogicalDeviceId, string DeviceDefinitionId, string Region)
        {
            try
            {
                if (!string.IsNullOrEmpty(DeviceSerialNumber) && !string.IsNullOrEmpty(LogicalDeviceId) &&
                    !string.IsNullOrEmpty(Region) && !string.IsNullOrEmpty(DeviceDefinitionId))
                {
                    var device = new Device
                    {
                        LinkedAccessKey = new Guid(AccessKey),
                        Action = Action,
                        DeviceId = DeviceDefinitionId,
                        DeviceSerialNumber = DeviceSerialNumber,
                        LogicalDeviceId = LogicalDeviceId,
                        RegionId = Region,
                        IsLocked = false,
                    };

                    var addedDevice = await _deviceRepo.Add(device);
                    if (addedDevice.ConfigureResponseID == "LogicalDeviceId")
                    {
                       

                        HttpContext.Current.Response.ContentType = "text/plain";
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "A Device with the attribute LogicalDeviceId equal to " + device.LogicalDeviceId + " already exists");
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                        return ResponseMessage(response);

                    }
                    else if (addedDevice.ConfigureResponseID == "DeviceSerialNumber")
                    {
                        HttpContext.Current.Response.ContentType = "text/plain";
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "A Device with the attribute DeviceSerialNumber equal to " + device.DeviceSerialNumber + " already exists");
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                        return ResponseMessage(response);
                    }
                    else
                    {
                        var ConfigureResponse = new ConfigureResponseID { Id = addedDevice.ConfigureResponseID };
                        var jetStream = new Jetstream1
                        {
                            ConfigureResponse = ConfigureResponse,
                            Header = ""
                        };
                        return Ok(jetStream);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [XmlRoot("JetStream")]
        public partial class Jetstream
        {
            [XmlElement]
            public string Header { get; set; }

            public ConfigureResponseID ConfigureResponse { get; set; }


        }

        public class ConfigureResponseID
        {
            [XmlAttribute("Id")]
            public string Id { get; set; }

        }

        public class ErrorID
        {
            [XmlAttribute("Id")]
            public string Id { get; set; }

        }

        [XmlRoot("String", Namespace = "https://SolsticeDataServices.tersosolutions.com/v1.2/application/")]
        public class StrError
        {
            [XmlElement]
            public string ErrorMsg { get; set; }

            public ErrorID ErrorResponse { get; set; }

        }


        [XmlRoot("SolsticeDataServices", Namespace = "https://SolsticeDataServices.tersosolutions.com/v1.2/application/")]
        public partial class Jetstream1
        {
            [XmlElement]
            public string Header { get; set; }

            public ConfigureResponseID ConfigureResponse { get; set; }


        }

    }

}