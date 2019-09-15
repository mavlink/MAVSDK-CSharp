using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Telemetry;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK_CSharp.Plugins
{
    public class Telemetry
    {
        private readonly TelemetryService.TelemetryServiceClient _telemetryServiceClient;

        internal Telemetry(Channel channel)
        {
            _telemetryServiceClient = new TelemetryService.TelemetryServiceClient(channel);
        }

        public IObservable<Position> Position()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribePosition(new SubscribePositionRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<Position> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.Position);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<Position> Home()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeHome(new SubscribeHomeRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<Position> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.Home);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<bool> InAir()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeInAir(new SubscribeInAirRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<bool> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.IsInAir);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<bool> Armed()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeArmed(new SubscribeArmedRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<bool> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.IsArmed);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<Quaternion> AttitudeQuaternion()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeAttitudeQuaternion(new SubscribeAttitudeQuaternionRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<Quaternion> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.AttitudeQuaternion);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<EulerAngle> AttitudeEuler()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeAttitudeEuler(new SubscribeAttitudeEulerRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<EulerAngle> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.AttitudeEuler);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<Quaternion> CameraAttitudeQuaternion()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeCameraAttitudeQuaternion(new SubscribeCameraAttitudeQuaternionRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<Quaternion> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.AttitudeQuaternion);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<EulerAngle> CameraAttitudeEuler()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeCameraAttitudeEuler(new SubscribeCameraAttitudeEulerRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<EulerAngle> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.AttitudeEuler);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<SpeedNed> GroundSpeedNed()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeGroundSpeedNed(new SubscribeGroundSpeedNedRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<SpeedNed> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.GroundSpeedNed);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<GpsInfo> GpsInfo()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeGpsInfo(new SubscribeGpsInfoRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<GpsInfo> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.GpsInfo);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<Battery> Battery()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeBattery(new SubscribeBatteryRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<Battery> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.Battery);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<FlightMode> FlightMode()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeFlightMode(new SubscribeFlightModeRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<FlightMode> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.FlightMode);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<Health> Health()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeHealth(new SubscribeHealthRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<Health> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.Health);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<RcStatus> RcStatus()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeRcStatus(new SubscribeRcStatusRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<RcStatus> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.RcStatus);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<StatusText> StatusText()
        {
            return Observable.Using(() => _telemetryServiceClient.SubscribeStatusText(new SubscribeStatusTextRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<StatusText> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.StatusText);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }
    }
}