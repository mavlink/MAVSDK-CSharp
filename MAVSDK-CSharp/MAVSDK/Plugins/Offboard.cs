using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Offboard;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
    public class Offboard
    {
        private readonly OffboardService.OffboardServiceClient _offboardServiceClient;

        internal Offboard(Channel channel)
        {
            _offboardServiceClient = new OffboardService.OffboardServiceClient(channel);
        }

        public IObservable<Unit> Start()
        {
            return Observable.Create<Unit>(observer =>
            {
                var startResponse = _offboardServiceClient.Start(new StartRequest());
                var offboardResult = startResponse.OffboardResult;
                if (offboardResult.Result == OffboardResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> Stop()
        {
            return Observable.Create<Unit>(observer =>
            {
                var stopResponse = _offboardServiceClient.Stop(new StopRequest());
                var offboardResult = stopResponse.OffboardResult;
                if (offboardResult.Result == OffboardResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<bool> IsActive()
        {
            return Observable.Create<bool>(observer =>
            {
                var isActiveResponse = _offboardServiceClient.IsActive(new IsActiveRequest());
                observer.OnNext(isActiveResponse.IsActive);

                observer.OnCompleted();
                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetAttitude()
        {
            return Observable.Create<Unit>(observer =>
            {
                _offboardServiceClient.SetAttitude(new SetAttitudeRequest());
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetActuatorControl()
        {
            return Observable.Create<Unit>(observer =>
            {
                _offboardServiceClient.SetActuatorControl(new SetActuatorControlRequest());
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetAttitudeRate()
        {
            return Observable.Create<Unit>(observer =>
            {
                _offboardServiceClient.SetAttitudeRate(new SetAttitudeRateRequest());
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetPositionNed()
        {
            return Observable.Create<Unit>(observer =>
            {
                _offboardServiceClient.SetPositionNed(new SetPositionNedRequest());
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetVelocityBody()
        {
            return Observable.Create<Unit>(observer =>
            {
                _offboardServiceClient.SetVelocityBody(new SetVelocityBodyRequest());
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetVelocityNed()
        {
            return Observable.Create<Unit>(observer =>
            {
                _offboardServiceClient.SetVelocityNed(new SetVelocityNedRequest());
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }
    }

    public class OffboardException : Exception
    {
        public OffboardResult.Types.Result Result { get; }
        public string ResultStr { get; }

        public OffboardException(OffboardResult.Types.Result result, string resultStr)
        {
            Result = result;
            ResultStr = resultStr;
        }
    }
}