using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.API.NVR
{
    public class IpCamaraSystem
    {
        public List<IpCameraChannel> Channels { get; set; } = new List<IpCameraChannel>();

        public IpCamaraSystem(int channels)
        {
            for(int i = 0; i < channels; i++)
            {
                this.Channels.Add(new IpCameraChannel() { ChannelId = i + 1 });
            }
        }

        public void Open(string ip)
        {
            foreach(IpCameraChannel channel in this.Channels)
            {
                channel.Start(ip);
            }
        }
    }
}
