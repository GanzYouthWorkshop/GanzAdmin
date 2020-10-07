using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GanzAdmin.API.NVR
{
    public class IpOnvifPtzCameraSystem
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public string Name { get; set; }
        public string Url { get; set; } = "rtsp://admin:admin@192.168.31.63:554/onvif{0}";
        public string PtzUrl { get; set; } = "http://192.168.31.63:5000/onvif/ptz_service";
        public List<IpCameraChannel> Channels { get; set; } = new List<IpCameraChannel>();

        public IpOnvifPtzCameraSystem(int channels)
        {
            for (int i = 0; i < channels; i++)
            {
                this.Channels.Add(new IpCameraChannel
                {
                    HandlerName = this.Name,
                    HandlerUrl = this.Url,
                    ChannelId = i + 1
                });
            }
        }

        public void Open()
        {
            foreach (IpCameraChannel channel in this.Channels)
            {
                channel.HandlerName = this.Name;
                channel.Start();
            }
        }

        public async void Ptz(float x, float y, float zoom)
        {
            string soap = String.Join("", new string[]
            {
                "<soap:Envelope xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:wsdl=\"http://www.onvif.org/ver20/ptz/wsdl\" xmlns:sch=\"http://www.onvif.org/ver10/schema\">",
                "   <soap:Header/>",
                "   <soap:Body>",
                "      <wsdl:ContinuousMove>",
                "         <wsdl:ProfileToken>?</wsdl:ProfileToken>",
                "         <wsdl:Velocity>",
                $"            <sch:PanTilt x=\"{x}\" y=\"{y}\" space=\"30\"/>",
                "            <sch:Zoom x=\"-2\" space=\"30\"/>",
                "         </wsdl:Velocity>",
                "         <wsdl:Timeout>500</wsdl:Timeout>",
                "      </wsdl:ContinuousMove>",
                "   </soap:Body>",
                "</soap:Envelope>"
            });

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.PtzUrl);
            request.Content = new StringContent(soap);
            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            request.Headers.Add("User-Agent", "GanzAdminCamService");
            request.Content.Headers.Clear();
            request.Content.Headers.Add("Content-Type", "application/soap+xml;charset=UTF-8;action=\"http://www.onvif.org/ver20/ptz/wsdl/ContinuousMove\"");

            var response = await HttpClient.SendAsync(request);
        }
    }
}
