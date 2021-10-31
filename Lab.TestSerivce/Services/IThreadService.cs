using System;

namespace Lab.TestService.Services
{
    /// <summary>
    /// 執行緒服務基底介面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IThreadService<T> : IDisposable
    {
        /// <summary>
        /// 將自訂運算排入佇列
        /// </summary>
        /// <param name="anything"></param>
        void Enqueue(T anything);
    }
}
