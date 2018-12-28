using System;
using consumer.email.consumers;
using MassTransit;

namespace consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var busControl = ConfigureBus();
            busControl.Start();

            Console.WriteLine("Waiting for new users...");
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

                cfg.ReceiveEndpoint(host, "Company.Users.Events.UserCreated.Email", endpoint =>
                {
                    endpoint.Consumer<UserCreatedConsumer>();
                });
            });
        }
    }

    
}