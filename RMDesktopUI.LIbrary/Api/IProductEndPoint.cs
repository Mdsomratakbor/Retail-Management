using RMDekstopUI.Library.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDesktopUI.LIbrary.Api
{
    public interface IProductEndPoint
    {
        Task<List<ProductModel>> GetAll();
    }
}