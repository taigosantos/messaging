using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using consumer.Contracts;
using consumer.Entities;
using ClientService.Contracts;
using MassTransit;

namespace consumer.Consumers
{
    public class CheckOrderStatusConsumer : IConsumer<CheckOrderStatus>
    {
        private readonly ICollection<Order> _orders;

        public CheckOrderStatusConsumer()
        {
            _orders = new List<Order>
            {
                new Order { Id = 1, DataCriacao = new DateTime(2018, 1, 18), Status = OrderStatusType.Enviada },
                new Order { Id = 2, DataCriacao = new DateTime(2018, 3, 8), Status = OrderStatusType.Processada },
                new Order { Id = 3, DataCriacao = new DateTime(2018, 8, 23), Status = OrderStatusType.Paga },
            };
        }

        public async Task Consume(ConsumeContext<CheckOrderStatus> context)
        {
            Console.WriteLine($"Showing data for {context.Message.Id}");

            var order = _orders.FirstOrDefault(x => x.Id == context.Message.Id);

            if (order == null)
            {
                await context.RespondAsync<IMessageResponse<OrderStatus>>(new {});
                return;
            }

            await context.RespondAsync<IMessageResponse<OrderStatus>>(new
            {
                Data = new
                {
                    Id = order.Id,
                        DataCriacao = order.DataCriacao,
                        Status = order.Status
                }
            });
        }
    }
}