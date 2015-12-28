using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Configuration;
using System.IO;

namespace SolsticeDataServices.UserInterface.Model
{
    public static class ResponseUtility
    {
        internal static string GetRespose(string Type, string Params)
        {
            HttpResponseMessage Response = null;
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/xml"));

            string BaseUrl = ConfigurationManager.AppSettings["APIUrl"].ToString();
            string RequestUrl = BaseUrl + Type + "?" + Params;

            Response = client.GetAsync(RequestUrl).Result;
            var stream = Response.Content.ReadAsStringAsync().Result;


            return stream + "**" + Response.ReasonPhrase;
        }
    }
}