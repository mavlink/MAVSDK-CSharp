using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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