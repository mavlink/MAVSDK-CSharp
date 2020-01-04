using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Camera;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
    public class Camera
    {
        private readonly CameraService.CameraServiceClient _cameraServiceClient;

        internal Camera(Channel channel)
        {
            _cameraServiceClient = new CameraService.CameraServiceClient(channel);
        }

        public IObservable<Unit> TakePhoto()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new TakePhotoRequest();
                var takePhotoResponse = _cameraServiceClient.TakePhoto(request);
                var cameraResult = takePhotoResponse.CameraResult;
                if (cameraResult.Result == CameraResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> StartPhotoInterval(float intervalS)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new StartPhotoIntervalRequest();
                request.IntervalS = intervalS;
                var startPhotoIntervalResponse = _cameraServiceClient.StartPhotoInterval(request);
                var cameraResult = startPhotoIntervalResponse.CameraResult;
                if (cameraResult.Result == CameraResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> StopPhotoInterval()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new StopPhotoIntervalRequest();
                var stopPhotoIntervalResponse = _cameraServiceClient.StopPhotoInterval(request);
                var cameraResult = stopPhotoIntervalResponse.CameraResult;
                if (cameraResult.Result == CameraResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> StartVideo()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new StartVideoRequest();
                var startVideoResponse = _cameraServiceClient.StartVideo(request);
                var cameraResult = startVideoResponse.CameraResult;
                if (cameraResult.Result == CameraResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> StopVideo()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new StopVideoRequest();
                var stopVideoResponse = _cameraServiceClient.StopVideo(request);
                var cameraResult = stopVideoResponse.CameraResult;
                if (cameraResult.Result == CameraResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> StartVideoStreaming()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new StartVideoStreamingRequest();
                var startVideoStreamingResponse = _cameraServiceClient.StartVideoStreaming(request);
                var cameraResult = startVideoStreamingResponse.CameraResult;
                if (cameraResult.Result == CameraResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> StopVideoStreaming()
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new StopVideoStreamingRequest();
                var stopVideoStreamingResponse = _cameraServiceClient.StopVideoStreaming(request);
                var cameraResult = stopVideoStreamingResponse.CameraResult;
                if (cameraResult.Result == CameraResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<Unit> SetMode(CameraMode cameraMode)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetModeRequest();
                request.CameraMode = cameraMode;
                var setModeResponse = _cameraServiceClient.SetMode(request);
                var cameraResult = setModeResponse.CameraResult;
                if (cameraResult.Result == CameraResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }

        public IObservable<CameraMode> Mode()
        {
            return Observable.Using(() => _cameraServiceClient.SubscribeMode(new SubscribeModeRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<CameraMode> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.CameraMode);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<VideoStreamInfo> VideoStreamInfo()
        {
            return Observable.Using(() => _cameraServiceClient.SubscribeVideoStreamInfo(new SubscribeVideoStreamInfoRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<VideoStreamInfo> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.VideoStreamInfo);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<CaptureInfo> CaptureInfo()
        {
            return Observable.Using(() => _cameraServiceClient.SubscribeCaptureInfo(new SubscribeCaptureInfoRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<CaptureInfo> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.CaptureInfo);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<CameraStatus> CameraStatus()
        {
            return Observable.Using(() => _cameraServiceClient.SubscribeCameraStatus(new SubscribeCameraStatusRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<CameraStatus> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.CameraStatus);
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<List<Setting>> CurrentSettings()
        {
            return Observable.Using(() => _cameraServiceClient.SubscribeCurrentSettings(new SubscribeCurrentSettingsRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<List<Setting>> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.CurrentSettings.ToList());
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<List<SettingOptions>> PossibleSettingOptions()
        {
            return Observable.Using(() => _cameraServiceClient.SubscribePossibleSettingOptions(new SubscribePossibleSettingOptionsRequest()).ResponseStream,
                reader => Observable.Create(
                    async (IObserver<List<SettingOptions>> observer) =>
                    {
                        try
                        {
                            while (await reader.MoveNext())
                            {
                                observer.OnNext(reader.Current.SettingOptions.ToList());
                            }
                            observer.OnCompleted();
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }
                    }));
        }

        public IObservable<Unit> SetSetting(Setting setting)
        {
            return Observable.Create<Unit>(observer =>
            {
                var request = new SetSettingRequest();
                request.Setting = setting;
                var setSettingResponse = _cameraServiceClient.SetSetting(request);
                var cameraResult = setSettingResponse.CameraResult;
                if (cameraResult.Result == CameraResult.Types.Result.Success)
                {
                    observer.OnCompleted();
                }
                else
                {
                    observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
                }

                return Task.FromResult(Disposable.Empty);
            });
        }
    }

    public class CameraException : Exception
    {
        public CameraResult.Types.Result Result { get; }
        public string ResultStr { get; }

        public CameraException(CameraResult.Types.Result result, string resultStr)
        {
            Result = result;
            ResultStr = resultStr;
        }
    }
}