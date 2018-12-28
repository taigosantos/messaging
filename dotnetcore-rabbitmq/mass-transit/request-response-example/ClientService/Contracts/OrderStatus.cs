using System;

namespace consumer.Contracts
{
    public interface OrderStatus
    {
        int Id { get; }
        DateTime DataCriacao { get; }
        OrderStatusType Status { get; }
    }

    public enum OrderStatusType : short
    {
        Processada,
        Paga,
        Enviada
    }
}