using AskPam.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskPam.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthInfo> Login(Login login);
    }
}
