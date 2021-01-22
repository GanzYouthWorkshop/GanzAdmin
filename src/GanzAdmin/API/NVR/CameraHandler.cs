using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.API.NVR
{
    public class CameraHandler
    {
        public static CameraHandler Instance { get; private set; }

        public static void Start()
        {
            Instance = new CameraHandler();
        }

        public IpCameraSystem G2Cameras { get; set; }
        public IpOnvifPtzCameraSystem G1MobileCamera { get; set; }

        public CameraHandler()
        {
            this.G2Cameras = new IpCameraSystem(4)
            {
                Name = "g2"
            };

            this.G1MobileCamera = new IpOnvifPtzCameraSystem(1)
            {
                Name = "g1"
            };
        }
    }
}
