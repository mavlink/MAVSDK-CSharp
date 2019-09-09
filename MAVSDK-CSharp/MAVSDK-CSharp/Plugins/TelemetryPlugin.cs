using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Grpc.Core;
using Mavsdk.Rpc.Telemetry;

namespace MAVSDK_CSharp.Plugins
{
    public class TelemetryPlugin
    {
        private readonly TelemetryService.TelemetryServiceClient _telemetryClient;

        public TelemetryPlugin(string host, string port)
        {
            var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _telemetryClient = new TelemetryService.TelemetryServiceClient(channel);
        }

        public IObservable<Position> Position()
        {
            return Observable.Using(() => _telemetryClient.SubscribePosition(new SubscribePositionRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<Position> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.Position);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }
    }
}