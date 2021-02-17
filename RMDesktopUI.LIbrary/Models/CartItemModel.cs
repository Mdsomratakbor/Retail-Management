using RMDekstopUI.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.LIbrary.Models
{
    public class CartItemModel
    {
        public ProductModel Product { get; set; }
        public int QuantityCart { get; set; }
        public string DisplayText
        {
            get
            {
                return $"{Product.ProductName}({QuantityCart})";
            }
        }
    }
}
