using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml.Serialization;
using DataService.API.Controllers;
using DataService.API.Utility;

namespace DataService.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new CustomNamespaceXmlFormatter { UseXmlSerializer = true });
            var XmlFormatter = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            XmlFormatter.SetSerializer<DeviceController.Jetstream>(new XmlSerializer(typeof(DeviceController.Jetstream)));
            XmlFormatter.SetSerializer<DeviceController.Jetstream1>(new XmlSerializer(typeof(DeviceController.Jetstream1)));
            XmlFormatter.SetSerializer<DeviceController.StrError>(new XmlSerializer(typeof(DeviceController.StrError)));

        }
    }
}
