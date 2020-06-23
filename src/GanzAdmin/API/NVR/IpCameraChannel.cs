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
                return this.m_FfmpegProcess != null && this.m_FfmpegProcess.Responding && !this.m_FfmpegProcess.HasExited;
            }
        }

        public string HandlerName { get; set; }
        public string HandlerUrl { get; set; }
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
                        Arguments = String.Join(' ', new string[]
                        {
                             "-fflags nobuffer",
                             "-rtsp_transport tcp",

                             $"-i {this.HandlerUrl}",

                             "-vsync 0",
                             "-copyts",
                             "-vcodec copy",
                             "-movflags frag_keyframe+empty_moov",
                             "-an",
                             "-hls_flags delete_segments+append_list",
                             "-f segment",
                             "-segment_list_flags live",
                             "-segment_time 1",
                             "-segment_list_size 3",
                             "-segment_format mpegts",
                             @$"-segment_list .\wwwroot\content\nvr\{this.HandlerName}_{this.ChannelId}.m3u8",
                             "-segment_list_type m3u8",
                             "-segment_list_entry_prefix /stream/",
                             @$".\wwwroot\content\nvr\{this.HandlerName}_{this.ChannelId}_%d.ts",
                        }),
                        CreateNoWindow = true,
                    }
                };
                this.m_FfmpegProcess.Start();
            }
        }
    }
}
