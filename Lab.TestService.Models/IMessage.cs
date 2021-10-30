namespace Lab.TestService.Models
{
    public interface IMessage<THeader, TPayload>
    {
        THeader Header { get; set; }
        TPayload Payload { get; set; }
    }

    public interface IRequest<THeader, TPayload> : IMessage<THeader, TPayload>
        where THeader : IRequestHeader
    {
    }

    public interface IResponse<THeader, TPayload> : IMessage<THeader, TPayload>
        where THeader : IResponseHeader
    {
    }
}
