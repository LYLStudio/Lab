namespace Lab.TestService
{
    using Lab.TestService.DoSomething.Models;
    using Lab.TestService.DoSomething.Services;


    public class Service : IService
    {
        public TestExpandoRs TestExpandoRq(TestExpandoRq request)
        {
            return new Manager().TestExpandoRq(request);
        }
    }
}
