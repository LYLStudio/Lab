using System;

namespace Lab.TestService.Models
{
    public interface IRequestHeader
    {
        string BatchID { get; set; }
        string MsgID { get; set; }
        string Action { get; set; }
        string FrnName { get; set; }
        string UserID { get; set; }
        string ClientIP { get; set; }
        string ServerIP { get; set; }
        DateTime? RequestTime { get; set; }
    }

    public interface IResponseHeader
    {
        bool IsOK { get; set; }
        string MsgID { get; set; }
        string StatusCode { get; set; }
        string Message { get; set; }
        string ProcessIP { get; set; }
        DateTime? ResponseTime { get; set; }
    }

    public class RequestHeader : IRequestHeader
    {
        public string BatchID { get; set; }
        public string MsgID { get; set; }
        public string Action { get; set; }
        public string FrnName { get; set; }
        public string UserID { get; set; }
        public string ClientIP { get; set; }
        public string ServerIP { get; set; }
        public DateTime? RequestTime { get; set; }
    }

    public class ResponseHeader : IResponseHeader
    {
        //public string BatchID { get; set; }
        //public string MsgID { get; set; }
        //public string Action { get; set; }
        //public string FrnName { get; set; }
        //public string UserID { get; set; }
        //public string ClientIP { get; set; }
        //public string ServerIP { get; set; }
        //public DateTime? RequestTime { get; set; }

        public bool IsOK { get; set; }
        public string MsgID { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public string ProcessIP { get; set; }
        public DateTime? ResponseTime { get; set; }

        //public static implicit operator BaseResponseHeader(RequestHeader header)
        //{
        //    var result = new BaseResponseHeader()
        //    {
        //        BatchID = header.BatchID,
        //        MsgID = header.MsgID,
        //        Action = header.Action,
        //        FrnName = header.FrnName,
        //        UserID = header.UserID,
        //        ClientIP = header.ClientIP,
        //        ServerIP = header.ServerIP,
        //        RequestTime = header.RequestTime,
        //    };

        //    return result;
        //}
    }
}