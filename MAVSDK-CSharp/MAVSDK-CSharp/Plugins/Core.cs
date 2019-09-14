using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Core;

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

        




    }
}