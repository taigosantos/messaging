using System;

namespace Company.Users.Events
{
    public interface UserCreated
    {
        Guid Id { get; }
        string Name { get; }
    }
}