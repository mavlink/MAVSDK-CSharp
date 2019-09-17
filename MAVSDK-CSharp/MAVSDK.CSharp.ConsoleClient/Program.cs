using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MAVSDK;

namespace MAVSDK.CSharp.ConsoleClient
{
    class Program
    {
        private const string Host = "localhost";
        private const int Port = 50051;

        static async Task Main()
        {
            /*
             * Print the relative altitude.
             *
             * Note:
             *     - round it to the first decimal
             *     - emit an event only when the value changes
             *     - discard the altitudes lower than 0
             */
            var drone = new MavSystem(Host, Port);
            drone.Telemetry.Position()
                .Select(position => Math.Round(position.RelativeAltitudeM, 1))
                .DistinctUntilChanged()
                .Where(altitude => altitude >= 0)
                .Subscribe(Observer.Create<double>(altitude => Console.WriteLine($"altitude: {altitude}"), _ => { }));
            
            // Print the takeoff altitude.
            drone.Action.GetTakeoffAltitude()
                .Do(altitude => Console.WriteLine($"Takeoff altitude: {altitude}"))
                .Subscribe();
            
            // Arm, takeoff, wait 5 seconds and land.
            var tcs = new TaskCompletionSource<bool>();
            drone.Action.Arm()
                .Concat(drone.Action.Takeoff())
                .Delay(TimeSpan.FromSeconds(5))
                .Concat(drone.Action.Land())
                .Subscribe(Observer.Create<Unit>(_ => { }, onError: _ => tcs.SetResult(false), onCompleted: () => tcs.SetResult(true)));

            // Wait until the takeoff routine completes (which happens when the landing starts)
            await tcs.Task;
        }
    }
}
