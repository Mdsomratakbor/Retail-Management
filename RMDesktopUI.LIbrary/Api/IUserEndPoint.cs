using RMDesktopUI.LIbrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDesktopUI.LIbrary.Api
{
    public interface IUserEndPoint
    {
        Task<List<UserModel>> GetAll();
    }
}