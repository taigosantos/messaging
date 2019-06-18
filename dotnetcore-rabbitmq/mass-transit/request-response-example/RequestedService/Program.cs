using System;
using consumer.Consumers;
using consumer.Contracts;
using GreenPipes;
using MassTransit;

namespace consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var busControl = ConfigureBus();
            busControl.Start();

            Console.WriteLine("Service Running...");
            Console.ReadLine();
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

                cfg.ReceiveEndpoint(host, "checkorderstatus", endpoint =>
                {
                    endpoint.Consumer<CheckOrderStatusConsumer>();
                    endpoint.UseCircuitBreaker(cb =>
                    {
                        cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                        cb.TripThreshold = 15;
                        cb.ActiveThreshold = 10;
                        cb.ResetInterval = TimeSpan.FromMinutes(5);
                    });
                });
            });
        }

    }

}