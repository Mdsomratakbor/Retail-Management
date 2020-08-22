using RMDesktopUI.LIbrary.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace RMDesktopUI.LIbrary.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticateUser> Authenticate(string userName, string password);
        Task GetLoggedInUserInfo(string token);
    }
}