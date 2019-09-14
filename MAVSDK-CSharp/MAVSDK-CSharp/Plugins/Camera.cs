using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Camera;

namespace MAVSDK_CSharp.Plugins
{
    public class Camera
    {
        private readonly CameraService.CameraServiceClient _cameraServiceClient;

        public Camera(string host, string port)
        {
            var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _cameraServiceClient = new CameraService.CameraServiceClient(channel);
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

        public IObservable<Unit> TakePhoto()
        {
            return Observable.Create<Unit>(observer =>
            {
                var takePhotoResponse = _cameraServiceClient.TakePhoto(new TakePhotoRequest());
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

        public IObservable<Unit> StartPhotoInterval()
        {
            return Observable.Create<Unit>(observer =>
            {
                var startPhotoIntervalResponse = _cameraServiceClient.StartPhotoInterval(new StartPhotoIntervalRequest());
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
                var stopPhotoIntervalResponse = _cameraServiceClient.StopPhotoInterval(new StopPhotoIntervalRequest());
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
                var startVideoResponse = _cameraServiceClient.StartVideo(new StartVideoRequest());
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
                var stopVideoResponse = _cameraServiceClient.StopVideo(new StopVideoRequest());
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
                var startVideoStreamingResponse = _cameraServiceClient.StartVideoStreaming(new StartVideoStreamingRequest());
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
                var stopVideoStreamingResponse = _cameraServiceClient.StopVideoStreaming(new StopVideoStreamingRequest());
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

        public IObservable<Unit> SetMode()
        {
            return Observable.Create<Unit>(observer =>
            {
                var setModeResponse = _cameraServiceClient.SetMode(new SetModeRequest());
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

        public IObservable<Unit> SetSetting()
        {
            return Observable.Create<Unit>(observer =>
            {
                var setSettingResponse = _cameraServiceClient.SetSetting(new SetSettingRequest());
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
}