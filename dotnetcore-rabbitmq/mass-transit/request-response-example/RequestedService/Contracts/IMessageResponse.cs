namespace ClientService.Contracts
{
    public interface IMessageResponse<T>
    {
        T Data { get; }
    }
}