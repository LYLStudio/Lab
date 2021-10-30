using System;

namespace Lab.TestService.Services
{
    public interface IManager : IExceptionHandled
    {
    }

    public interface IExceptionHandled
    {
        void Execute<T>(IAnything<T> anything);
    }

    public abstract class ManagerBase : IManager
    {
        public abstract void Execute<T>(IAnything<T> anything);
    }
    public interface IAnything<T>
    {
        T Parameter { get; set; }
        Action<T> Callback { get; set; }
    }

    public class Anything<T> : IAnything<T>
    {
        public T Parameter { get; set; }
        public Action<T> Callback { get; set; }
    }
}