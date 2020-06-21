using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VseTut.Core.Auth.Dto;

namespace VseTut.Core.Auth
{
    public interface IAuthManager
    {
        Task<AuthenticateResultModel> LogInAsync(string emailAddress, string password);

        Task<AuthenticateResultModel> RegisterAsync(string emailAddress, string password, string userName);
    }
}
