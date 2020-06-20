using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.API.NVR
{
    public class IpCameraChannel
    {
        public bool Runnning
        {
            get
            {
                return false; // this.m_FfmpegProcess?.Responding && !this.m_FfmpegProcess.HasExited;
            }
        }
        public int ChannelId { get; set; }

        private Process m_FfmpegProcess;

        public void Start(string ip)
        {
            if(!this.Runnning)
            {
                this.m_FfmpegProcess = new Process
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = ".\\ffmpeg.exe",
                        Arguments = "",
                        CreateNoWindow = true,
                    }
                };
                this.m_FfmpegProcess.Start();
            }
        }
    }
}
