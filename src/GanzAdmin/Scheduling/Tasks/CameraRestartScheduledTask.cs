using GanzAdmin.API.NVR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.Scheduling
{
    public class CameraRestartScheduledTask : ScheduledTaskBase
    {
        public CameraRestartScheduledTask()
        {
            this.RunSchedule = new TimeSpan(1, 0, 0, 0, 0);
        }

        public override bool Run(DateTime datetime)
        {
            Console.WriteLine("Closing all camera handlers...");
            CameraHandler.Instance.G1MobileCamera.Close();
            CameraHandler.Instance.G2Cameras.Close();

            Console.WriteLine("Deleting all leftover files...");
            List<IpCameraChannel> allchannels = new List<IpCameraChannel>();
            allchannels.AddRange(CameraHandler.Instance.G2Cameras.Channels);
            allchannels.AddRange(CameraHandler.Instance.G1MobileCamera.Channels);
            foreach(IpCameraChannel channel in allchannels)
            {
                channel.DeleteOldFiles();
            }

            Console.WriteLine("Restaring all camera handlers...");
            CameraHandler.Instance.G1MobileCamera.Open();
            CameraHandler.Instance.G2Cameras.Open();

            return true;
        }
    }
}
