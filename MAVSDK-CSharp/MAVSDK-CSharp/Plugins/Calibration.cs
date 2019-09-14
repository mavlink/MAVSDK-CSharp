using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Calibration;

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