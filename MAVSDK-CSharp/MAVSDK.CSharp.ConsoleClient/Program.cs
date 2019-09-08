using System.Threading.Tasks;

namespace MAVSDK.CSharp.ConsoleClient
{
	class Program
	{
		private const string Host = "127.0.0.1";
		private const string Port = "50051";

		static async Task Main(string[] args)
		{
			var actionPlugin = new ActionPlugin(Host,Port);


			try
			{
				await actionPlugin.Arm();
				await actionPlugin.Takeoff();
			}
			catch (System.Exception ex)
			{

				throw;
			}
		}
	}
}
