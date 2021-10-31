namespace Lab.TestService.Services
{
    using Lab.TestService.Models;

    public interface IManager : IPreprocess
    {
    }

    public interface IPreprocess
    {
        void Preprocess<T>(IAnything<T> anything);
    }

    public abstract class ManagerBase : IManager
    {
        protected ManagerBase() { }
        protected ManagerBase(ILogService logger) : this() { }

        public abstract void Preprocess<T>(IAnything<T> anything);
    }
}