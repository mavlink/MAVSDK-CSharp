using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Param;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
    public class Param
    {
        private readonly ParamService.ParamServiceClient _paramServiceClient;

        internal Param(Channel channel)
        {
            _paramServiceClient = new ParamService.ParamServiceClient(channel);
        }

        public IObservable<int> GetIntParam()
        {
            return Observable.Create<int>(observer =>
            {
                var getIntParamResponse = _paramServiceClient.GetIntParam(new GetIntParamRequest());
                var paramResult = getIntParamResponse.ParamResult;
                if (paramResult.Result == ParamResult.Types.Result.Success)
                {
                    observer.OnNext(getIntParamResponse.Value);
                }
                else
                {
                    observer.OnError(new ParamException(paramResult.Result, paramResult.ResultStr));
                }

                observer.OnCompleted();
                return Task.FromResult(Disposable.Empty);
            });
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

        public IObservable<float> GetFloatParam()
        {
            return Observable.Create<float>(observer =>
            {
                var getFloatParamResponse = _paramServiceClient.GetFloatParam(new GetFloatParamRequest());
                var paramResult = getFloatParamResponse.ParamResult;
                if (paramResult.Result == ParamResult.Types.Result.Success)
                {
                    observer.OnNext(getFloatParamResponse.Value);
                }
                else
                {
                    observer.OnError(new ParamException(paramResult.Result, paramResult.ResultStr));
                }

                observer.OnCompleted();
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
}