using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Action;

namespace MAVSDK.CSharp.ConsoleClient
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
			var actionServiceClient = new ActionService.ActionServiceClient(channel);

			var armResponse = await actionServiceClient.ArmAsync(new ArmRequest(), new CallOptions());
			if (armResponse.ActionResult.Result == ActionResult.Types.Result.Success)
			{
				await actionServiceClient.TakeoffAsync(new TakeoffRequest());
			}
		}
	}
}
