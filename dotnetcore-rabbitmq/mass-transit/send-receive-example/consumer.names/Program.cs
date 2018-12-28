using System;
using Company.Mensagens;
using MassTransit;

namespace consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var busControl = ConfigureBus();
            busControl.Start();

            Console.WriteLine("Waiting for names...");
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

                cfg.ReceiveEndpoint(host, "Company.Messages.Names", endpoint =>
                {
                    endpoint.Handler<Mensagem>(async context =>
                    {
                        await Console.Out.WriteLineAsync($"Received Name: {context.Message.Texo}");
                    });
                });
                
            });
        }
    }

    
}