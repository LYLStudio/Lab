using System;

namespace Lab.TestService.Models
{
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

    public class AnythingDynamic : Anything<dynamic>
    {
    }
}
