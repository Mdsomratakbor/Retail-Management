using RMDekstopUI.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace RMDekstopUI.Helper
{
    public interface IAPIHelper
    {
        Task<AuthenticateUser> Authenticate(string userName, string password);
    }
}