using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Mission;

namespace MAVSDK_CSharp.Plugins
{
    public class Mission
    {
        private readonly MissionService.MissionServiceClient _missionServiceClient;

        public Mission(string host, string port)
        {
            var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _missionServiceClient = new MissionService.MissionServiceClient(channel);
        }

        public class MissionException : Exception
        {
            public MissionResult.Types.Result Result { get; }
            public string ResultStr { get; }

            public MissionException(MissionResult.Types.Result result, string resultStr)
            {
                Result = result;
                ResultStr = resultStr;
            }
        }

        public IObservable<Unit> UploadMission()
        {
            return Observable.Create<Unit>(observer =>
            {
                var uploadMissionResponse = _missionServiceClient.UploadMission(new UploadMissionRequest());
                var missionResult = uploadMissionResponse.MissionResult;
                if (missionResult.Result == MissionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> CancelMissionUpload()
        {
            return Observable.Create<Unit>(observer =>
            {
                _missionServiceClient.CancelMissionUpload(new CancelMissionUploadRequest());
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }



        public IObservable<Unit> CancelMissionDownload()
        {
            return Observable.Create<Unit>(observer =>
            {
                _missionServiceClient.CancelMissionDownload(new CancelMissionDownloadRequest());
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> StartMission()
        {
            return Observable.Create<Unit>(observer =>
            {
                var startMissionResponse = _missionServiceClient.StartMission(new StartMissionRequest());
                var missionResult = startMissionResponse.MissionResult;
                if (missionResult.Result == MissionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> PauseMission()
        {
            return Observable.Create<Unit>(observer =>
            {
                var pauseMissionResponse = _missionServiceClient.PauseMission(new PauseMissionRequest());
                var missionResult = pauseMissionResponse.MissionResult;
                if (missionResult.Result == MissionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetCurrentMissionItemIndex()
        {
            return Observable.Create<Unit>(observer =>
            {
                var setCurrentMissionItemIndexResponse = _missionServiceClient.SetCurrentMissionItemIndex(new SetCurrentMissionItemIndexRequest());
                var missionResult = setCurrentMissionItemIndexResponse.MissionResult;
                if (missionResult.Result == MissionResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }



        public IObservable<MissionProgress> MissionProgress()
        {
            return Observable.Using(() => _missionServiceClient.SubscribeMissionProgress(new SubscribeMissionProgressRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<MissionProgress> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.MissionProgress);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }



        public IObservable<Unit> SetReturnToLaunchAfterMission()
        {
            return Observable.Create<Unit>(observer =>
            {
                _missionServiceClient.SetReturnToLaunchAfterMission(new SetReturnToLaunchAfterMissionRequest());
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }
    }
}