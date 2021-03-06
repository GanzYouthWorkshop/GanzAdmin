﻿using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.API.NVR
{
    public class IpCameraSystem
    {
        public string Name { get; set; }
        public string Url { get; set; } = "rtsp://192.168.100.254:554/user=admin&password=&channel={0}&stream=1.sdp";
        public List<IpCameraChannel> Channels { get; set; } = new List<IpCameraChannel>();

        public IpCameraSystem(int channels)
        {
            for(int i = 0; i < channels; i++)
            {
                this.Channels.Add(new IpCameraChannel
                {
                    HandlerName = this.Name,
                    HandlerUrl = this.Url,
                    ChannelId = i + 1,
                    Protocol = "tcp"
                });
            }
        }

        public void Open()
        {
            foreach(IpCameraChannel channel in this.Channels)
            {
                channel.HandlerName = this.Name;
                channel.Start();
            }

            //<video id="video-player" controls preload="none">
            // < source src = "/stream/index.m3u8" type = "application/x-mpegURL" >
            //</ video >
        }

        public void Close()
        {
            foreach (IpCameraChannel channel in this.Channels)
            {
                channel.HandlerName = this.Name;
                channel.Stop();
            }
        }
    }
}
