using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Action;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
    public class Action
    {
        private readonly ActionService.ActionServiceClient _actionServiceClient;

        internal Action(Channel channel)
        {
            _actionServiceClient = new ActionService.ActionServiceClient(channel);
        }

        public IObservable<Unit> Arm()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new ArmRequest();
                var armResponse = _actionServiceClient.Arm(request);
                var actionResult = armResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> Disarm()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new DisarmRequest();
                var disarmResponse = _actionServiceClient.Disarm(request);
                var actionResult = disarmResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> Takeoff()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new TakeoffRequest();
                var takeoffResponse = _actionServiceClient.Takeoff(request);
                var actionResult = takeoffResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> Land()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new LandRequest();
                var landResponse = _actionServiceClient.Land(request);
                var actionResult = landResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> Reboot()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new RebootRequest();
                var rebootResponse = _actionServiceClient.Reboot(request);
                var actionResult = rebootResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> Kill()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new KillRequest();
                var killResponse = _actionServiceClient.Kill(request);
                var actionResult = killResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> ReturnToLaunch()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new ReturnToLaunchRequest();
                var returnToLaunchResponse = _actionServiceClient.ReturnToLaunch(request);
                var actionResult = returnToLaunchResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> TransitionToFixedWing()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new TransitionToFixedWingRequest();
                var transitionToFixedWingResponse = _actionServiceClient.TransitionToFixedWing(request);
                var actionResult = transitionToFixedWingResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> TransitionToMulticopter()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new TransitionToMulticopterRequest();
                var transitionToMulticopterResponse = _actionServiceClient.TransitionToMulticopter(request);
                var actionResult = transitionToMulticopterResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<float> GetTakeoffAltitude()
        {
            return Observable.Create<float>(observer =>
            {
                var request = new GetTakeoffAltitudeRequest();
                var getTakeoffAltitudeResponse = _actionServiceClient.GetTakeoffAltitude(request);
                var actionResult = getTakeoffAltitudeResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnNext(getTakeoffAltitudeResponse.Altitude);
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                observer.OnCompleted();
                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetTakeoffAltitude(float altitude)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetTakeoffAltitudeRequest();
                request.Altitude = altitude;
                var setTakeoffAltitudeResponse = _actionServiceClient.SetTakeoffAltitude(request);
                var actionResult = setTakeoffAltitudeResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<float> GetMaximumSpeed()
        {
            return Observable.Create<float>(observer =>
            {
                var request = new GetMaximumSpeedRequest();
                var getMaximumSpeedResponse = _actionServiceClient.GetMaximumSpeed(request);
                var actionResult = getMaximumSpeedResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnNext(getMaximumSpeedResponse.Speed);
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                observer.OnCompleted();
                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetMaximumSpeed(float speed)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetMaximumSpeedRequest();
                request.Speed = speed;
                var setMaximumSpeedResponse = _actionServiceClient.SetMaximumSpeed(request);
                var actionResult = setMaximumSpeedResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<float> GetReturnToLaunchAltitude()
        {
            return Observable.Create<float>(observer =>
            {
                var request = new GetReturnToLaunchAltitudeRequest();
                var getReturnToLaunchAltitudeResponse = _actionServiceClient.GetReturnToLaunchAltitude(request);
                var actionResult = getReturnToLaunchAltitudeResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnNext(getReturnToLaunchAltitudeResponse.RelativeAltitudeM);
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                observer.OnCompleted();
                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetReturnToLaunchAltitude(float relativeAltitudeM)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetReturnToLaunchAltitudeRequest();
                request.RelativeAltitudeM = relativeAltitudeM;
                var setReturnToLaunchAltitudeResponse = _actionServiceClient.SetReturnToLaunchAltitude(request);
                var actionResult = setReturnToLaunchAltitudeResponse.ActionResult;
                if (actionResult.Result == ActionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new ActionException(actionResult.Result, actionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }
    }

    public class ActionException : Exception
    {
        public ActionResult.Types.Result Result { get; }
        public string ResultStr { get; }

        public ActionException(ActionResult.Types.Result result, string resultStr)
        {
            Result = result;
            ResultStr = resultStr;
        }
    }
}