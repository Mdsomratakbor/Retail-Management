using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDekstopUI.Models
{
    public class CartItemDisplayModel : INotifyPropertyChanged
    {
        public ProductDisplayModel Product { get; set; }
        private int _quantityCart;
        public int QuantityCart
        {
            get { return _quantityCart; }
            set
            {
                _quantityCart = value;
                CallPropertyChanged(nameof(QuantityCart));
                CallPropertyChanged(nameof(DisplayText));
            }
        }
        public string DisplayText
        {
            get
            {
                return $"{Product.ProductName}({QuantityCart})";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void CallPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
