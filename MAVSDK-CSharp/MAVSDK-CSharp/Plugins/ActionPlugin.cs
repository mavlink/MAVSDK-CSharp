using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Action;

namespace MAVSDK_CSharp.Plugins
{
    public class ActionPlugin
    {
        private readonly ActionService.ActionServiceClient _actionServiceClient;

        public ActionPlugin(string host, string port)
        {
            var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _actionServiceClient = new ActionService.ActionServiceClient(channel);
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

                return Disposable.Empty;
            });
        }
    }
}
