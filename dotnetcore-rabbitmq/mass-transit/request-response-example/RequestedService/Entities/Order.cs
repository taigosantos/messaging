using System;

namespace consumer.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public OrderStatusType Status { get; set; }
    }

    public enum OrderStatusType : short
    {
        Processada,
        Paga,
        Enviada
    }
}