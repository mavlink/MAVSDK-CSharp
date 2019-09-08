using System.Reactive;
using System.Threading.Tasks;
using MAVSDK_CSharp.Plugins;

namespace MAVSDK.CSharp.ConsoleClient
{
	class Program
	{
		private const string Host = "192.168.43.140";
		private const string Port = "50051";

		static async Task Main(string[] args)
		{
			var actionPlugin = new ActionPlugin(Host,Port);

			try
			{
				await actionPlugin.Arm();
				await actionPlugin.Takeoff();
				await Task.Delay(5000);

				actionPlugin.Land().Subscribe(Observer.Create<Unit>(_ => {}, _ => {}));
			}
			catch (System.Exception ex)
			{

				throw;
			}
		}
	}
}
