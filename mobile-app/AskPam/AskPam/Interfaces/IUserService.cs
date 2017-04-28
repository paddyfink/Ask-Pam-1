using AskPam.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AskPam.Interfaces
{
    public interface IUserService
    {
        Task<List<UserList>> GetAllUsers();
    }
}
