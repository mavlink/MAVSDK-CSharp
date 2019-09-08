using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Action;

namespace MAVSDK.CSharp.ConsoleClient
{
	public class ActionPlugin
	{
		private readonly ActionService.ActionServiceClient actionServiceClient;

		public ActionPlugin(string host, string port)
		{
			var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
			actionServiceClient = new ActionService.ActionServiceClient(channel);
		}

		public async Task Takeoff()
		{
			await actionServiceClient.TakeoffAsync(new TakeoffRequest());
		}

		public async Task Arm()
		{
			var armResponse = await actionServiceClient.ArmAsync(new ArmRequest());
			var actionResult = armResponse.ActionResult;
			var result = actionResult.Result == ActionResult.Types.Result.Success;
			if (result == false)
			{
				throw new ActionException(actionResult.Result, actionResult.ResultStr);
			}
		}
	}
}