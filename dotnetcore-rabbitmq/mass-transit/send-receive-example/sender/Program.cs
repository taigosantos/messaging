using System;
using Company.Mensagens;
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
                Console.WriteLine("Enter message (or quit to exit)");
                Console.Write("> ");
                string value = Console.ReadLine();

                if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                    break;

                var namesEndpoint = busControl
                    .GetSendEndpoint(new Uri("rabbitmq://fly.rmq.cloudamqp.com/umeawlvd/Company.Messages.Names"))
                    .Result;

                var numbersEndpoint = busControl
                    .GetSendEndpoint(new Uri("rabbitmq://fly.rmq.cloudamqp.com/umeawlvd/Company.Messages.Numbers"))
                    .Result;

                if (Int32.TryParse(value, out _))
                {
                    Console.WriteLine($"Sending number: {value}");
                    numbersEndpoint.Send<Mensagem>(new
                    {
                        Id = Guid.NewGuid(),
                        Texo = value
                    });
                }
                else
                {
                    Console.WriteLine($"Sending name: {value}");
                    namesEndpoint.Send<Mensagem>(new
                    {
                        Id = Guid.NewGuid(),
                        Texo = value
                    });
                }
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
    }
}