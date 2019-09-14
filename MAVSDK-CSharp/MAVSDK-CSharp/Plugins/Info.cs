using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Info;

namespace MAVSDK_CSharp.Plugins
{
    public class Info
    {
        private readonly InfoService.InfoServiceClient _infoServiceClient;

        public Info(string host, string port)
        {
            var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _infoServiceClient = new InfoService.InfoServiceClient(channel);
        }

        public class InfoException : Exception
        {
            public InfoResult.Types.Result Result { get; }
            public string ResultStr { get; }

            public InfoException(InfoResult.Types.Result result, string resultStr)
            {
                Result = result;
                ResultStr = resultStr;
            }
        }


    }
}