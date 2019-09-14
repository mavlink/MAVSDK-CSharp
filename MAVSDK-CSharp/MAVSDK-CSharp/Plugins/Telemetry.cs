using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Telemetry;

namespace MAVSDK_CSharp.Plugins
{
    public class Telemetry
    {
        private readonly TelemetryService.TelemetryServiceClient _telemetryServiceClient;

        public Telemetry(string host, string port)
        {
            var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _telemetryServiceClient = new TelemetryService.TelemetryServiceClient(channel);
        }

        






























    }
}