using RMDesktopUI.LIbrary.Models;
using System.Threading.Tasks;

namespace RMDesktopUI.LIbrary.Api
{
    public interface ISaleEndPoint
    {
        Task PostSale(SaleModel sale);
    }
}