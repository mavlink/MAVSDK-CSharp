using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Action = MAVSDK_CSharp.Plugins.Action;
using Console = System.Console;

namespace MAVSDK.CSharp.ConsoleClient
{
    class Program
    {
        private const string Host = "localhost";
        private const string Port = "50051";

        static async Task Main(string[] args)
        {
            /*
             * Print the relative altitude.
             *
             * Note:
             *     - round it to the first decimal
             *     - emit an event only when the value changes
             *     - discard the altitudes lower than 0
             */
            /*
            var telemetryPlugin = new TelemetryPlugin(Host, Port);
            telemetryPlugin.Position()
                .Select(position => Math.Round((double) position.RelativeAltitudeM, 1))
                .DistinctUntilChanged()
                .Where(altitude => altitude >= 0)
                .Subscribe(Observer.Create<double>(altitude => Console.WriteLine($"altitude: {altitude}"), _ => { }));
                */

            // Arm, takeoff, wait 5 seconds and land.
            var action = new Action(Host, Port);
            var tcs = new TaskCompletionSource<bool>();
            action.Arm()
                .Concat(action.Takeoff())
                .Delay(TimeSpan.FromSeconds(5))
                .Concat(action.Land())
                .Subscribe(Observer.Create<Unit>(_ => { }, () => tcs.SetResult(true)));

            // Wait until the takeoff routine completes (which happens when the landing starts)
            await tcs.Task;
        }
    }
}
