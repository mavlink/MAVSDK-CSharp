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
			return Observable.Generate(_telemetryClient.SubscribePosition(new SubscribePositionRequest()).ResponseStream, reader => reader.MoveNext().Result, reader => reader,
				reader => reader.Current.Position);
			// TODO wrap IAsyncStreamReader in a true async way
		}
	}
}