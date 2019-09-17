using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Core;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK_CSharp.Plugins
{
    public class Core
    {
        private readonly CoreService.CoreServiceClient _coreServiceClient;

        public Core(string host, string port)
        {
            var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _coreServiceClient = new CoreService.CoreServiceClient(channel);
        }

        

        public IObservable<ConnectionState> ConnectionState()
        {
            return Observable.Using(() => _coreServiceClient.SubscribeConnectionState(new SubscribeConnectionStateRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<ConnectionState> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.ConnectionState);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<List<PluginInfo>> ListRunningPlugins()
        {
            return Observable.Create<List<PluginInfo>>(observer =>
            {
                var listRunningPluginsResponse = _coreServiceClient.ListRunningPlugins(new ListRunningPluginsRequest());
                observer.OnNext(listRunningPluginsResponse.PluginInfo.ToList());

                observer.OnCompleted();
                return Task.FromResult(Disposable.Empty);
            });
        }
    }
}