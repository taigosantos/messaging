using System;
using consumer.Entities;

namespace consumer.Contracts
{
    public interface OrderStatus
    {
        int Id { get; }
        DateTime DataCriacao { get; }
        OrderStatusType Status { get; }
    }
}