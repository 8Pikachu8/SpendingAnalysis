using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpendingAnalysis.Aplication.Auth;

namespace SpendingAnalysis.Core.Abstractions
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(string username, string password);
        Task<AuthResponse> LoginAsync(string username, string password);
    }
}
