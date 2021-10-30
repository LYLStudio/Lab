using System.ServiceModel;

using Lab.TestService.DoSomething.Models;

namespace Lab.TestService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        TestExpandoRs TestExpandoRq(TestExpandoRq request);
    }
}
