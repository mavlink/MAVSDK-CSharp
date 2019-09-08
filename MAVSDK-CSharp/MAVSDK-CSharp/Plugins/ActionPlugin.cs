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

		public async Task Takeoff()
		{
			await _actionServiceClient.TakeoffAsync(new TakeoffRequest());
		}

		public async Task Arm()
		{
			var armResponse = await _actionServiceClient.ArmAsync(new ArmRequest());
			var actionResult = armResponse.ActionResult;
			var result = actionResult.Result == ActionResult.Types.Result.Success;
			if (result == false)
			{
				throw new ActionException(actionResult.Result, actionResult.ResultStr);
			}
		}

		public IObservable<Unit> Land()
		{
			return Observable.Create<Unit>(observer =>
			{
				var landResponse = _actionServiceClient.Land(new LandRequest()); //TODO async
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
	}
}