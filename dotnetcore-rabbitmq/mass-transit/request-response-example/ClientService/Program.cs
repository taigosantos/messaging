using System;
using System.Threading.Tasks;
using consumer.Contracts;
using ClientService.Contracts;
using MassTransit;

namespace sender
{

    class Program
    {
        static void Main(string[] args)
        {
            var busControl = ConfigureBus();
            busControl.Start();

            do
            {
                var client = CreateRequestClient(busControl);

                Console.WriteLine("Enter message (or quit to exit)");
                Console.Write("> ");
                string value = Console.ReadLine();

                if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                    break;

                Task.Run(async() =>
                {
                    var response = await client.Request(new { Id = Convert.ToInt32(value) });
                    var order = response?.Data;

                    if (order == null)
                    {
                        Console.WriteLine("Não encontrado");
                        return;
                    }

                    Console.WriteLine($"Order Id: {order.Id}");
                    Console.WriteLine($"Order DataCriacao: {order.DataCriacao}");
                    Console.WriteLine($"Order Status: {order.Status}");
                })
                .Wait();
            }
            while (true);

            busControl.Stop();
        }

        static IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://fly.rmq.cloudamqp.com/umeawlvd"), h =>
                {
                    h.Username("umeawlvd");
                    h.Password("O_CTKoSkU1FCHhYIydwlEeaO6NPk7ESE");
                });
            });
        }

        static IRequestClient<CheckOrderStatus, IMessageResponse<OrderStatus>> CreateRequestClient(IBusControl busControl)
        {
            var serviceAddress = new Uri("rabbitmq://fly.rmq.cloudamqp.com/umeawlvd/checkorderstatus");
            var client = busControl.CreateRequestClient<CheckOrderStatus, IMessageResponse<OrderStatus>>(
                    serviceAddress,
                    TimeSpan.FromSeconds(120)
                );

            return client;
        }
    }
}