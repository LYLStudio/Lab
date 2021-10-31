namespace Lab.TestService.DoSomething.Services
{
    using System;
    using System.Dynamic;

    using Lab.TestService.DoSomething.Models;
    using Lab.TestService.Models;
    using Lab.TestService.Services;

    public class Manager : ManagerBase
    {
        private ILogService _logger = null;

        public Manager(ILogService logger)
        {
            _logger = logger;
        }

        public override void Preprocess<T>(IAnything<T> anything)
        {
            Exception error = null;
            try
            {
                anything.Callback?.Invoke(anything.Parameter);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                var p = (dynamic)anything.Parameter;
                _logger.Log(new
                {
                    Request = p.RQ,
                    Response = p.RS,
                    Error = error
                });
            }
        }

        public TestExpandoRs TestExpandoRq(TestExpandoRq request)
        {
            var response = new TestExpandoRs()
            {
                Header = new ResponseHeader()
                {
                    MsgID = request.Header.MsgID,
                    Message = string.Empty,
                    ProcessIP = "127.0.0.2",
                    StatusCode = string.Empty
                },
                Payload = new ExpandoObject()
            };

            Preprocess(new AnythingDynamic()
            {
                Parameter = new { RQ = request, RS = response },
                Callback = (o) =>
                {
                    try
                    {
                        //Do Something
                        //o.RS.Payload.Balance = (decimal)o.RQ.Payload.Balance / 0;
                        o.RS.Payload.Balance = o.RQ.Payload.Balance - o.RQ.Payload.Amt;
                        o.RS.Payload.PostedDate = $"{DateTime.Now:yyyy-MM-dd}";

                        o.RS.Header.StatusCode = "0000";
                        o.RS.Header.Message = "TestOK";
                        o.RS.Header.ResponseTime = DateTime.Now;
                        o.RS.Header.IsOK = true;
                    }
                    catch (Exception ex)
                    {
                        o.RS.Header.StatusCode = "-999";
                        o.RS.Header.Message = string.IsNullOrWhiteSpace(o.RS.Header.Message) ? $"{ex.Message}" : string.Join("|", $"{o.RS.Header.Message}", $"{ex.Message}");
                        o.RS.Header.ResponseTime = DateTime.Now;

                        throw;
                    }
                }
            });

            return response;
        }
    }
}