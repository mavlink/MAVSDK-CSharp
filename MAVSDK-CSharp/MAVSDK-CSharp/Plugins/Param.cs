using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Param;

namespace MAVSDK_CSharp.Plugins
{
    public class Param
    {
        private readonly ParamService.ParamServiceClient _paramServiceClient;

        public Param(string host, string port)
        {
            var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _paramServiceClient = new ParamService.ParamServiceClient(channel);
        }

        public class ParamException : Exception
        {
            public ParamResult.Types.Result Result { get; }
            public string ResultStr { get; }

            public ParamException(ParamResult.Types.Result result, string resultStr)
            {
                Result = result;
                ResultStr = resultStr;
            }
        }



        public IObservable<Unit> SetIntParam()
        {
            return Observable.Create<Unit>(observer =>
            {
                var setIntParamResponse = _paramServiceClient.SetIntParam(new SetIntParamRequest());
                var paramResult = setIntParamResponse.ParamResult;
                if (paramResult.Result == ParamResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ParamException(paramResult.Result, paramResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }



        public IObservable<Unit> SetFloatParam()
        {
            return Observable.Create<Unit>(observer =>
            {
                var setFloatParamResponse = _paramServiceClient.SetFloatParam(new SetFloatParamRequest());
                var paramResult = setFloatParamResponse.ParamResult;
                if (paramResult.Result == ParamResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ParamException(paramResult.Result, paramResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }
    }
}