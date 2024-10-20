using MinimalApiCoink.Models;
using System.Collections.Generic;

namespace MinimalApiCoink.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}