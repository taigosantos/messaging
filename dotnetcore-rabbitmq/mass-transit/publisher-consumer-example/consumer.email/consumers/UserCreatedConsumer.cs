using System;
using System.Threading.Tasks;
using Company.Users.Events;
using MassTransit;

namespace consumer.email.consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var userCreated = context.Message;
            
            await Console.Out.WriteLineAsync($"User created");
            await Console.Out.WriteLineAsync($"E-mail sended to new user {userCreated.Name}");
        }
    }
}