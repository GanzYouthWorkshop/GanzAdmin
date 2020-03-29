using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GanzAdmin.ServerAdministration.Tasks
{
    public class UpdateDnsTask : ucAdminTask
    {
        private string m_ApiKey;

        public UpdateDnsTask(string name, TimeSpan updateInterval, string apiKey)
        {
            this.RunInterval = updateInterval;

            this.lblName.Text = name;
            this.lblRunInterval.Text = updateInterval.ToString("dd") + " days, " + updateInterval.ToString("hh") + " hours";
            this.m_ApiKey = apiKey;
        }

        protected override void RunInternal()
        {
            string url;
            HttpWebRequest request;
            WebResponse response;

            try
            {
                url = $"https://ganzadmin.nsupdate.info:{this.m_ApiKey}@ipv4.nsupdate.info/nic/delete";
                request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"ganzmuhely:{this.m_ApiKey}"));
                request.Method = "Get";
                response = request.GetResponse();
            }
            catch(Exception ex)
            {

            }

            try
            {

                url = $"https://ganzadmin.nsupdate.info:{this.m_ApiKey}@ipv6.nsupdate.info/nic/update";
                request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"ganzmuhely:{this.m_ApiKey}"));
                request.Method = "Get";
                response = request.GetResponse();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
