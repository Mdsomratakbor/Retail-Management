using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.LIbrary.Models
{
    public class SaleModel
    {
        public List<SaleDetailModel> SalesDetails { get; set; } = new List<SaleDetailModel>();
    }
}
