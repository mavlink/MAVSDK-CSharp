using System;
using Grpc.Core;
using MAVSDK.Plugins;
using Action = MAVSDK.Plugins.Action;

namespace MAVSDK
{
    public class MavsdkSystem: IDisposable
    {
        private readonly Channel _channel;

        public Action Action { get; }
        public Calibration Calibration { get; }
        public Camera Camera { get; }
        public Core Core { get; }
        public Gimbal Gimbal { get; }
        public Info Info { get; }
        public Mission Mission { get; }
        public Offboard Offboard { get; }
        public Param Param { get; }
        public Telemetry Telemetry { get; }

        public MavsdkSystem(string host, int port)
        {
            _channel = new Channel(host, port, ChannelCredentials.Insecure);

            Action = new Action(_channel);
            Calibration = new Calibration(_channel);
            Camera = new Camera(_channel);
            Core = new Core(_channel);
            Gimbal = new Gimbal(_channel);
            Info = new Info(_channel);
            Mission = new Mission(_channel);
            Offboard = new Offboard(_channel);
            Param = new Param(_channel);
            Telemetry = new Telemetry(_channel);
        }

        private void ReleaseUnmanagedResources()
        {
            try
            {
                _channel.ShutdownAsync().Wait(60000);
            }
            catch (Exception)
            {
                //ignored
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~MavsdkSystem()
        {
            ReleaseUnmanagedResources();
        }
    }
}