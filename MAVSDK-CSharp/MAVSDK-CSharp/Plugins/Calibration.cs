using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Calibration;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK_CSharp.Plugins
{
    public class Calibration
    {
        private readonly CalibrationService.CalibrationServiceClient _calibrationServiceClient;

        public Calibration(string host, string port)
        {
            var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _calibrationServiceClient = new CalibrationService.CalibrationServiceClient(channel);
        }

        public class CalibrationException : Exception
        {
            public CalibrationResult.Types.Result Result { get; }
            public string ResultStr { get; }

            public CalibrationException(CalibrationResult.Types.Result result, string resultStr)
            {
                Result = result;
                ResultStr = resultStr;
            }
        }

        public IObservable<ProgressData> CalibrateGyro()
        {
            return Observable.Using(() => _calibrationServiceClient.SubscribeCalibrateGyro(new SubscribeCalibrateGyroRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<ProgressData> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                var result = reader.Current.CalibrationResult;
                                switch (result.Result)
                                {
                                    case CalibrationResult.Types.Result.Success:
                                    case CalibrationResult.Types.Result.InProgress:
                                    case CalibrationResult.Types.Result.Instruction:
                                        observer.OnNext(reader.Current.ProgressData);
                                        break;
                                    default:
                                        observer.OnError(new CalibrationException(result.Result, result.ResultStr));
                                        break;
                                }
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<ProgressData> CalibrateAccelerometer()
        {
            return Observable.Using(() => _calibrationServiceClient.SubscribeCalibrateAccelerometer(new SubscribeCalibrateAccelerometerRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<ProgressData> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                var result = reader.Current.CalibrationResult;
                                switch (result.Result)
                                {
                                    case CalibrationResult.Types.Result.Success:
                                    case CalibrationResult.Types.Result.InProgress:
                                    case CalibrationResult.Types.Result.Instruction:
                                        observer.OnNext(reader.Current.ProgressData);
                                        break;
                                    default:
                                        observer.OnError(new CalibrationException(result.Result, result.ResultStr));
                                        break;
                                }
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<ProgressData> CalibrateMagnetometer()
        {
            return Observable.Using(() => _calibrationServiceClient.SubscribeCalibrateMagnetometer(new SubscribeCalibrateMagnetometerRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<ProgressData> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                var result = reader.Current.CalibrationResult;
                                switch (result.Result)
                                {
                                    case CalibrationResult.Types.Result.Success:
                                    case CalibrationResult.Types.Result.InProgress:
                                    case CalibrationResult.Types.Result.Instruction:
                                        observer.OnNext(reader.Current.ProgressData);
                                        break;
                                    default:
                                        observer.OnError(new CalibrationException(result.Result, result.ResultStr));
                                        break;
                                }
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<ProgressData> CalibrateGimbalAccelerometer()
        {
            return Observable.Using(() => _calibrationServiceClient.SubscribeCalibrateGimbalAccelerometer(new SubscribeCalibrateGimbalAccelerometerRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<ProgressData> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                var result = reader.Current.CalibrationResult;
                                switch (result.Result)
                                {
                                    case CalibrationResult.Types.Result.Success:
                                    case CalibrationResult.Types.Result.InProgress:
                                    case CalibrationResult.Types.Result.Instruction:
                                        observer.OnNext(reader.Current.ProgressData);
                                        break;
                                    default:
                                        observer.OnError(new CalibrationException(result.Result, result.ResultStr));
                                        break;
                                }
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<Unit> Cancel()
        {
            return Observable.Create<Unit>(observer =>
            {
                _calibrationServiceClient.Cancel(new CancelRequest());
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }
    }
}