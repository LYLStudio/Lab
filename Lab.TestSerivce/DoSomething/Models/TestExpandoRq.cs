namespace Lab.TestService.DoSomething.Models
{
    using System.Dynamic;

    using Lab.TestService.Models;

    public class TestExpandoRq : IRequest<RequestHeader, ExpandoObject>
    {
        public RequestHeader Header { get; set; }
        public ExpandoObject Payload { get; set; }
    }

    public class TestExpandoRs : IResponse<ResponseHeader, ExpandoObject>
    {
        public ResponseHeader Header { get; set; }
        public ExpandoObject Payload { get; set; }
    }
}
