using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResponseApiClient.Messaging.Users.Contracts
{
    public interface IUserDetails
    {
        int Id { get; }
        string Nome { get; }
        string Sobrenome { get; }
        string Email { get; }
    }
}
