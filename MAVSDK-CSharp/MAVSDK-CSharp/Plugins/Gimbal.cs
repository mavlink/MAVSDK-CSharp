using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Gimbal;

namespace MAVSDK_CSharp.Plugins
{
    public class Gimbal
    {
        private readonly GimbalService.GimbalServiceClient _gimbalServiceClient;

        public Gimbal(string host, string port)
        {
            var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _gimbalServiceClient = new GimbalService.GimbalServiceClient(channel);
        }

        public class GimbalException : Exception
        {
            public GimbalResult.Types.Result Result { get; }
            public string ResultStr { get; }

            public GimbalException(GimbalResult.Types.Result result, string resultStr)
            {
                Result = result;
                ResultStr = resultStr;
            }
        }

        public IObservable<Unit> SetPitchAndYaw()
        {
            return Observable.Create<Unit>(observer =>
            {
                var setPitchAndYawResponse = _gimbalServiceClient.SetPitchAndYaw(new SetPitchAndYawRequest());
                var gimbalResult = setPitchAndYawResponse.GimbalResult;
                if (gimbalResult.Result == GimbalResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new GimbalException(gimbalResult.Result, gimbalResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }
    }
}