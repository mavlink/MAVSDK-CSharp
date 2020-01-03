using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Info;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
    public class Info
    {
        private readonly InfoService.InfoServiceClient _infoServiceClient;

        internal Info(Channel channel)
        {
            _infoServiceClient = new InfoService.InfoServiceClient(channel);
        }

        public IObservable<Version> GetVersion()
        {
            return Observable.Create<Version>(observer =>
            {
                var request = new GetVersionRequest();
                var getVersionResponse = _infoServiceClient.GetVersion(request);
                var infoResult = getVersionResponse.InfoResult;
                if (infoResult.Result == InfoResult.Types.Result.Success)
                {
                    observer.OnNext(getVersionResponse.Version);
                }
                else
                {
                    observer.OnError(new InfoException(infoResult.Result, infoResult.ResultStr));
                }

                observer.OnCompleted();
                return Task.FromResult(Disposable.Empty);
            });
        }
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