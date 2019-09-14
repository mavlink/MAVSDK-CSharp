using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Action;

namespace MAVSDK_CSharp.Plugins
{
    public class Action
    {
        private readonly ActionService.ActionServiceClient _actionServiceClient;

        public Action(string host, string port)
        {
            var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _actionServiceClient = new ActionService.ActionServiceClient(channel);
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

        public IObservable<Unit> Arm()
        {
            return Observable.Create<Unit>(observer =>
            {
                var armResponse = _actionServiceClient.Arm(new ArmRequest());
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
                var disarmResponse = _actionServiceClient.Disarm(new DisarmRequest());
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
                var takeoffResponse = _actionServiceClient.Takeoff(new TakeoffRequest());
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
                var landResponse = _actionServiceClient.Land(new LandRequest());
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
                var rebootResponse = _actionServiceClient.Reboot(new RebootRequest());
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
                var killResponse = _actionServiceClient.Kill(new KillRequest());
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
                var returnToLaunchResponse = _actionServiceClient.ReturnToLaunch(new ReturnToLaunchRequest());
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
                var transitionToFixedWingResponse = _actionServiceClient.TransitionToFixedWing(new TransitionToFixedWingRequest());
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
                var transitionToMulticopterResponse = _actionServiceClient.TransitionToMulticopter(new TransitionToMulticopterRequest());
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



        public IObservable<Unit> SetTakeoffAltitude()
        {
            return Observable.Create<Unit>(observer =>
            {
                var setTakeoffAltitudeResponse = _actionServiceClient.SetTakeoffAltitude(new SetTakeoffAltitudeRequest());
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



        public IObservable<Unit> SetMaximumSpeed()
        {
            return Observable.Create<Unit>(observer =>
            {
                var setMaximumSpeedResponse = _actionServiceClient.SetMaximumSpeed(new SetMaximumSpeedRequest());
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



        public IObservable<Unit> SetReturnToLaunchAltitude()
        {
            return Observable.Create<Unit>(observer =>
            {
                var setReturnToLaunchAltitudeResponse = _actionServiceClient.SetReturnToLaunchAltitude(new SetReturnToLaunchAltitudeRequest());
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
}