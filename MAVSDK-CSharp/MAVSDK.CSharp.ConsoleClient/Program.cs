using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using MAVSDK_CSharp.Plugins;
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
            var telemetryPlugin = new TelemetryPlugin(Host, Port);
            telemetryPlugin.Position()
                .Select(position => Math.Round((double) position.RelativeAltitudeM, 1))
                .DistinctUntilChanged()
                .Where(altitude => altitude >= 0)
                .Subscribe(Observer.Create<double>(altitude => Console.WriteLine($"altitude: {altitude}"), _ => { }));

            // Arm, takeoff, wait 5 seconds and land.
            var actionPlugin = new ActionPlugin(Host, Port);
            var tcs = new TaskCompletionSource<bool>();
            actionPlugin.Arm()
                .Concat(actionPlugin.Takeoff())
                .Delay(TimeSpan.FromSeconds(5))
                .Concat(actionPlugin.Land())
                .Subscribe(Observer.Create<Unit>(_ => { }, () => tcs.SetResult(true)));

            // Wait until the takeoff routine completes (which happens when the landing starts)
            await tcs.Task;
        }
    }
}
