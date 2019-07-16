using MassTransit;
using ResponseApiClient.Domain;
using ResponseApiClient.Messaging.Users.Contracts;
using System;
using System.Threading.Tasks;

namespace ResponseApiClient.Messaging.Users.Consumers
{
    public class GetUserDetailsConsumer : IConsumer<IGetUserDetails>
    {
        private readonly UserRepository _userRepository;

        public GetUserDetailsConsumer()
        {
            _userRepository = new UserRepository();
        }

        public async Task Consume(ConsumeContext<IGetUserDetails> context)
        {
            var getUserDetails = context.Message;
            var user = _userRepository.GetUserDetails(getUserDetails.Id);

            // TODO evitar exception
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            await context.RespondAsync<IUserDetails>(new
            {
                Id = user.Id,
                Nome = user.Nome,
                Sobrenome = user.Sobrenome,
                Email = user.Email,
            });
        }
    }
}
