using System;

namespace Lab.TestService.Services
{
    public interface ILogService
    {
        void Log<T>(T item);
        void Log<T>(T item, Action<T> callback);
    }
}
