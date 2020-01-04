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
                var request = new StartRequest();
                var startResponse = _offboardServiceClient.Start(request);
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
                var request = new StopRequest();
                var stopResponse = _offboardServiceClient.Stop(request);
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
                var request = new IsActiveRequest();
                var isActiveResponse = _offboardServiceClient.IsActive(request);
                observer.OnNext(isActiveResponse.IsActive);

                observer.OnCompleted();
                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetAttitude(Attitude attitude)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetAttitudeRequest();
                request.Attitude = attitude;
                _offboardServiceClient.SetAttitude(request);
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetActuatorControl(ActuatorControl actuatorControl)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetActuatorControlRequest();
                request.ActuatorControl = actuatorControl;
                _offboardServiceClient.SetActuatorControl(request);
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetAttitudeRate(AttitudeRate attitudeRate)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetAttitudeRateRequest();
                request.AttitudeRate = attitudeRate;
                _offboardServiceClient.SetAttitudeRate(request);
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetPositionNed(PositionNEDYaw positionNedYaw)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetPositionNedRequest();
                request.PositionNedYaw = positionNedYaw;
                _offboardServiceClient.SetPositionNed(request);
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetVelocityBody(VelocityBodyYawspeed velocityBodyYawspeed)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetVelocityBodyRequest();
                request.VelocityBodyYawspeed = velocityBodyYawspeed;
                _offboardServiceClient.SetVelocityBody(request);
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetVelocityNed(VelocityNEDYaw velocityNedYaw)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetVelocityNedRequest();
                request.VelocityNedYaw = velocityNedYaw;
                _offboardServiceClient.SetVelocityNed(request);
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