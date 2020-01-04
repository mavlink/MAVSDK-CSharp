using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Mavsdk.Rpc.Mission;

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
            var drone = new MavsdkSystem(Host, Port);
            var tcs = new TaskCompletionSource<bool>();

            drone.Telemetry.Position()
                .Select(position => Math.Round(position.RelativeAltitudeM, 1))
                .DistinctUntilChanged()
                .Where(altitude => altitude >= 0)
                .Subscribe(Observer.Create<double>(altitude => Console.WriteLine($"altitude: {altitude}"), _ => { }));
            
            // Print the takeoff altitude.
            drone.Action.GetTakeoffAltitude()
                .Do(altitude => Console.WriteLine($"Takeoff altitude: {altitude}"))
                .Subscribe();

            // Print mission progress + end program when flown to completion
            drone.Mission.MissionProgress()
                .Subscribe(mp =>
                {
                    Console.WriteLine($"Mission progress - item #{mp.CurrentItemIndex+1}");
                    if (mp.CurrentItemIndex == mp.MissionCount - 1 && mp.MissionCount > 0)
                    {
                        tcs.SetResult(true);
                    }
                });
            
            // Upload and fly a mission
            var missionPoints = await GetSampleMissionPoints(drone);
            drone.Mission.UploadMission(missionPoints)
                .Concat(drone.Mission.SetReturnToLaunchAfterMission(true))
                .Concat(drone.Action.Arm())
                .Concat(drone.Mission.StartMission())
                .Subscribe(_ => { });

            //wait until the mission finishes (from MissionProgress subscription)
            await tcs.Task;
        }

        private static async Task<List<MissionItem>> GetSampleMissionPoints(MavsdkSystem drone)
        {
            var dronePosition = await drone.Telemetry.Position().FirstAsync();
            var missionPoints = new List<MissionItem>();
            var missionItem = new MissionItem();
            missionItem.IsFlyThrough = true;
            missionItem.SpeedMS = 2;
            missionItem.RelativeAltitudeM = 5;
            missionItem.LatitudeDeg = dronePosition.LatitudeDeg;
            missionItem.LongitudeDeg = dronePosition.LongitudeDeg;

            for (int i = 0; i < 3; i++)
            {
                missionItem = missionItem.Clone();
                if (i % 2 == 0)
                {
                    missionItem.LatitudeDeg += 0.0001;
                }
                else
                {
                    missionItem.LatitudeDeg -= 0.0001;
                }

                missionItem.LongitudeDeg += 0.0001;
                missionPoints.Add(missionItem);
            }

            return missionPoints;
        }
    }
}
