using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResponseApiClient.Domain
{
    public class User
    {
        public User(int id, string nome, string sobrenome, string email)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Email { get; private set; }
    }
}
