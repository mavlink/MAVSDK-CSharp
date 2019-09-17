using System;
using Grpc.Core;
using MAVSDK_CSharp.Plugins;
using Action = MAVSDK_CSharp.Plugins.Action;

namespace MAVSDK_CSharp
{
    public class MavSystem: IDisposable
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

        public MavSystem(string host, int port)
        {
            _channel = new Channel(host, port, ChannelCredentials.Insecure);
            Telemetry = new Telemetry(_channel);
            Action = new Action(_channel);
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

        ~MavSystem()
        {
            ReleaseUnmanagedResources();
        }
    }
}