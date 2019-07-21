using Grpc.Core;
using Mavsdk.Rpc.Action;

namespace MAVSDK.CSharp.ConsoleClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var channel = new Channel("127.0.0.1:50052", ChannelCredentials.Insecure);
			var actionServiceClient = new ActionService.ActionServiceClient(channel);

			actionServiceClient.Arm(new ArmRequest(), new CallOptions());
		}
	}
}
