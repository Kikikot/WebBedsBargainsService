using Externals.WebApiSystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebBedsBargariansService.UnitTests._Emulation
{
    internal class EmulatedWebApi : IWebApi
    {
        private static bool _forceTimeOut = false;

        private static string _result = null;

        public void SetForceTimeOut(bool forceTimeOut)
        {
            _forceTimeOut = forceTimeOut;
        }

        public void SetResult(object result)
        {
            _result = JsonConvert.SerializeObject(result);
        }

        public T Get<T>(string url, double? timeOutMilliSeconds = null)
        {
            T response = default;

            var tasks = new List<Task> { Task.Factory.StartNew(() => RunGet(url, timeOutMilliSeconds, out response)) };

            if (timeOutMilliSeconds.HasValue && timeOutMilliSeconds.Value > 0)
                Task.WaitAll(tasks.ToArray(), TimeSpan.FromMilliseconds(timeOutMilliSeconds.Value));

            else
                Task.WaitAll(tasks.ToArray());

            return response;
        }

        private void RunGet<T>(string url, double? timeOutMilliSeconds, out T response)
        {
            if (_forceTimeOut && timeOutMilliSeconds.HasValue && timeOutMilliSeconds.Value > 0)
                Thread.Sleep(TimeSpan.FromMilliseconds(timeOutMilliSeconds.Value + 1000));

            response = JsonConvert.DeserializeObject<T>(_result);
        }
    }
}
