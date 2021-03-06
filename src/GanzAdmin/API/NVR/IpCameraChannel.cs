﻿using GanzAdmin.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GanzAdmin.API.NVR
{
    public class IpCameraChannel
    {
        public bool Runnning
        {
            get
            {
                try
                {
                    return this.m_FfmpegProcess != null && !this.m_FfmpegProcess.HasExited;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public string HandlerName { get; set; }
        public string HandlerUrl { get; set; }
        public int ChannelId { get; set; }
        public string Protocol { get; set; }

        private Process m_FfmpegProcess;
        private Thread m_FsWatcher;

        //ffmpeg -i rtsp://192.168.100.254:554/user=admin^&password=^&channel=4^&stream=1.sdp?real_stream--rtp-caching=100 -c:v libx264 -crf 21 -preset veryfast -c:a aac -b:a 128k -ac 2 -f hls -hls_time 4 -hls_playlist_type event -segment_wrap 5 -hls_flags delete_segments  stream.m3u8
        public void Start()
        {
            if (!this.Runnning)
            {
                string streamDirectory = GanzAdminConfiguration.Instance.NvrFolder;
                string streamFile = $"{this.HandlerName}_{this.ChannelId}.m3u8";

                string[] arguments = new string[]
                {
                    "-fflags nobuffer",
                    $"-rtsp_transport {this.Protocol}",

                    $"-i {String.Format(this.HandlerUrl, this.ChannelId)}",

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
                    @$"-segment_list {streamDirectory}\{streamFile}",
                    "-segment_list_type m3u8",
                    "-segment_list_entry_prefix /content/nvr/",
                    @$"{streamDirectory}\{this.HandlerName}_{this.ChannelId}_%d.ts",
                };

                string args = String.Join(' ', arguments);
                this.m_FfmpegProcess = new Process
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = "ffmpeg.exe",
                        Arguments = args,
                        CreateNoWindow = false,
                    }
                };
                this.m_FfmpegProcess.Start();

                this.m_FsWatcher = new Thread(new ThreadStart(this.DeleteOldFilesRunning));
                this.m_FsWatcher.Start();
            }
        }

        public void Stop()
        {
            if (this.m_FfmpegProcess != null)
            {
                this.m_FfmpegProcess.Kill();

                //while (!this.m_FfmpegProcess.HasExited)
                {
                    //this.m_FfmpegProcess.Refresh();
                    Thread.Sleep(100);
                }
            }
        }

        private void DeleteOldFilesRunning()
        {
            while(this.Runnning)
            {
                this.DeleteOldFiles();
                Thread.Sleep(10000);
            }
        }

        public void DeleteOldFiles()
        {
            string streamFile = $@"{GanzAdminConfiguration.Instance.NvrFolder}\{this.HandlerName}_{this.ChannelId}.m3u8";
            string[] files = Directory.GetFiles(GanzAdminConfiguration.Instance.NvrFolder).Where(s => s.Contains($"{this.HandlerName}_{this.ChannelId}")).ToArray();
            DateTime deletionThreshold = DateTime.Now.AddMinutes(-5);

            int counter = 0;
            if (File.Exists(streamFile))
            {
                string streamFileContents = File.ReadAllText(streamFile);

                List<string> orderedFiles = files.Where(f => !streamFileContents.Contains(f.Split('\\').Last()) && !f.Contains("m3u8")).OrderByDescending(f => f).Skip(4).ToList();

                foreach (string file in orderedFiles)
                {
                    if (File.Exists(file))
                    {
                        try
                        {
                            File.Delete(file);
                            counter++;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            Console.WriteLine($"Régi kamerafájlok ({this.HandlerName}/{this.ChannelId}) közül {counter} törölve");
        }
    }
}
