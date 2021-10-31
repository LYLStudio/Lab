using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using Lab.TestService.Models;
using Lab.TestService.Services;

namespace Lab.TestService.Logger.Services
{
    public class LogService : LogServiceBase
    {
        public override void Log<T>(T item, Action<T> callback)
        {
            callback?.Invoke(item);
            Log(item);
        }
    }

    public abstract class LogServiceBase : ILogService
    {
        protected readonly ThreadService _thread = null;

        public LogServiceBase()
        {
            _thread = new ThreadService(this.GetType().FullName);
        }

        public abstract void Log<T>(T item, Action<T> callback);

        public virtual void Log<T>(T item)
        {
            _thread.Enqueue(new AnythingDynamic()
            {
                Parameter = item,
                Callback = (o) =>
                {                    
                    var path = Path.Combine(GlobalParameter.PATH_LOG, $"{DateTime.Now:yyyy-MM-dd.HH}.log");
                    if (!Directory.Exists(GlobalParameter.PATH_LOG))
                        Directory.CreateDirectory(GlobalParameter.PATH_LOG);

                    var logItem = Newtonsoft.Json.JsonConvert.SerializeObject(new { Time = $"{DateTime.Now:O}", Data = o });
                    using (var sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine(logItem);
                        sw.Close();
                    }
                }
            });
        }

        protected class ThreadService : IThreadService<AnythingDynamic>
        {
            private Dictionary<ThreadPriority, ConcurrentQueue<AnythingDynamic>> _queueList;
            private AutoResetEvent _resetEvent;
            private Thread _thread;
            private bool _isTerminateService = false;

            public event EventHandler<Exception> ExceptionOccurred;

            public string Id { get; }
            public ThreadPriority Priority { get; }
            public int Sleep { get; set; }

            #region IDisposable Support
            private bool disposedValue = false; // 偵測多餘的呼叫

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: 處置 Managed 狀態 (Managed 物件)。
                        if (_queueList != null)
                        {
                            _queueList.Clear();
                            _queueList = null;
                        }

                        if (_resetEvent != null)
                        {
                            _resetEvent.Dispose();
                            _resetEvent = null;
                        }

                        if (_thread != null)
                        {
                            _thread = null;
                        }
                    }

                    disposedValue = true;
                }
            }

            public void Dispose()
            {
                Dispose(true);
                // GC.SuppressFinalize(this);
            }
            #endregion

            public ThreadService(string id, int sleep = 1)
            {
                Id = id;
                Priority = ThreadPriority.BelowNormal;
                Sleep = sleep;

                _queueList = new Dictionary<ThreadPriority, ConcurrentQueue<AnythingDynamic>>(Enum.GetNames(typeof(ThreadPriority)).Length)
                {
                    { Priority, new ConcurrentQueue<AnythingDynamic>() }
                };

                _resetEvent = new AutoResetEvent(false);
                _thread = new Thread(Start)
                {
                    Name = Id,
                    Priority = Priority,
                    IsBackground = true
                };

                _thread.Start();
            }

            public void Enqueue(AnythingDynamic anything)
            {
                if (!_isTerminateService)
                {
                    try
                    {
                        _queueList[Priority].Enqueue(anything);
                        _resetEvent.Set();
                    }
                    catch (Exception ex)
                    {
                        OnExceptionOccurred(ex);
                    }
                }
            }

            public void Stop()
            {
                _isTerminateService = true;
            }

            private void Start()
            {
                while (true && !_isTerminateService)
                {
                    if (!_queueList[Priority].IsEmpty)
                    {
                        Dequeue();
                    }
                    else
                    {
                        _resetEvent.WaitOne();
                    }

                    _resetEvent.WaitOne(Sleep);
                }

                while (!_queueList[Priority].IsEmpty)
                {
                    Dequeue();
                    _resetEvent.WaitOne(Sleep);
                }
            }

            private void Dequeue()
            {
                try
                {
                    if (_queueList[Priority].TryDequeue(out AnythingDynamic anything))
                    {
                        anything.Callback(anything.Parameter);
                    }
                }
                catch (Exception ex)
                {
                    OnExceptionOccurred(ex);
                }
            }

            private void OnExceptionOccurred(Exception exception)
            {
                ExceptionOccurred?.Invoke(this, exception);
            }

        }

    }
}