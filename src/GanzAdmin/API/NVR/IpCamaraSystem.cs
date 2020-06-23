using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.API.NVR
{
    public class IpCamaraSystem
    {
        public string Name { get; set; }
        public string Url { get; set; } = "rtsp://192.168.1.20:10554/user=admin_password=tlJwpbo6_channel=1_stream=0.sdp";
        public List<IpCameraChannel> Channels { get; set; } = new List<IpCameraChannel>();

        public IpCamaraSystem(int channels)
        {
            for(int i = 0; i < channels; i++)
            {
                this.Channels.Add(new IpCameraChannel
                {
                    HandlerName = this.Name,
                    HandlerUrl = this.Url,
                    ChannelId = i + 1
                });
            }
        }

        public void Open(string ip)
        {
            foreach(IpCameraChannel channel in this.Channels)
            {
                channel.Start(ip);
            }

            //<video id="video-player" controls preload="none">
            // < source src = "/stream/index.m3u8" type = "application/x-mpegURL" >
            //</ video >
        }
    }
}
