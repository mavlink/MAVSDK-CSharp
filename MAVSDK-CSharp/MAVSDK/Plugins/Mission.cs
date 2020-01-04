using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Mission;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
    public class Mission
    {
        private readonly MissionService.MissionServiceClient _missionServiceClient;

        internal Mission(Channel channel)
        {
            _missionServiceClient = new MissionService.MissionServiceClient(channel);
        }

        public IObservable<Unit> UploadMission(List<MissionItem> missionItems)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new UploadMissionRequest();
                request.MissionItems.AddRange(missionItems);
                var uploadMissionResponse = _missionServiceClient.UploadMission(request);
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
                var request = new CancelMissionUploadRequest();
                _missionServiceClient.CancelMissionUpload(request);
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<List<MissionItem>> DownloadMission()
        {
            return Observable.Create<List<MissionItem>>(observer =>
            {
                var request = new DownloadMissionRequest();
                var downloadMissionResponse = _missionServiceClient.DownloadMission(request);
                var missionResult = downloadMissionResponse.MissionResult;
                if (missionResult.Result == MissionResult.Types.Result.Success)
                {
                    observer.OnNext(downloadMissionResponse.MissionItems.ToList());
                }
                else
                {
                    observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
                }

                observer.OnCompleted();
                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> CancelMissionDownload()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new CancelMissionDownloadRequest();
                _missionServiceClient.CancelMissionDownload(request);
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> StartMission()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new StartMissionRequest();
                var startMissionResponse = _missionServiceClient.StartMission(request);
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
                var request = new PauseMissionRequest();
                var pauseMissionResponse = _missionServiceClient.PauseMission(request);
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

        public IObservable<Unit> SetCurrentMissionItemIndex(int index)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetCurrentMissionItemIndexRequest();
                request.Index = index;
                var setCurrentMissionItemIndexResponse = _missionServiceClient.SetCurrentMissionItemIndex(request);
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

        public IObservable<bool> IsMissionFinished()
        {
            return Observable.Create<bool>(observer =>
            {
                var request = new IsMissionFinishedRequest();
                var isMissionFinishedResponse = _missionServiceClient.IsMissionFinished(request);
                observer.OnNext(isMissionFinishedResponse.IsFinished);

                observer.OnCompleted();
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

        public IObservable<bool> GetReturnToLaunchAfterMission()
        {
            return Observable.Create<bool>(observer =>
            {
                var request = new GetReturnToLaunchAfterMissionRequest();
                var getReturnToLaunchAfterMissionResponse = _missionServiceClient.GetReturnToLaunchAfterMission(request);
                observer.OnNext(getReturnToLaunchAfterMissionResponse.Enable);

                observer.OnCompleted();
                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetReturnToLaunchAfterMission(bool enable)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetReturnToLaunchAfterMissionRequest();
                request.Enable = enable;
                _missionServiceClient.SetReturnToLaunchAfterMission(request);
                observer.OnCompleted();

                return Task.FromResult(Disposable.Empty);
            });
        }
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
}