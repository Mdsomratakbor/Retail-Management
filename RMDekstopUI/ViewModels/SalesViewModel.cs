using Caliburn.Micro;
using RMDekstopUI.Library.ViewModels;
using RMDesktopUI.LIbrary.Api;
using RMDesktopUI.LIbrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDekstopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private IProductEndPoint _productEndPoint;

        public SalesViewModel(IProductEndPoint productEndPoint)
        {
            _productEndPoint = productEndPoint;

        }
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProduts();

        }
        private async Task LoadProduts()
        {
            var productList = await _productEndPoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }
        private BindingList<ProductModel> _products;
        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }


        public ProductModel _selectedProduct;
        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private BindingList<CartItemModel>  _cart =new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }


        private int _itemQuantity = 1;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }


        public string SubTotal
        {
            get
            {
                // TODO - Replace with Calculation
                decimal subTotal = 0;
                foreach (var item in Cart)
                {
                    subTotal += item.Product.RetailPrice * item.QuantityCart;
                }
                return subTotal.ToString("C");
            }
        }
        public string Tax
        {
            get
            {
                decimal taxAmount = 0;
                foreach (var item in Cart)
                {
                    taxAmount += ();
                }
                return taxAmount.ToString("C");
            }
        }
        public string Total
        {
            get
            {
                // TODO - Replace with Calculation
                return "$0.00";
            }
        }
        public bool CanAddToCart
        {
            get
            {
                bool output = false;


                // Make sure something is selected
                // Make sure ther is an item quantity
                if (ItemQuantity > 0 && SelectedProduct?.QuantityStock >= ItemQuantity)
                {
                    output = true;
                }
                else
                {

                }
                return output;
            }

        }
        public void AddToCart()
        {
            CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);
            if(existingItem!= null)
            {
                existingItem.QuantityCart += ItemQuantity;
                SelectedProduct.QuantityStock -= ItemQuantity;
                //// HACK : There should be a better way of refreshing the cart display
               Cart.Remove(existingItem);
               Cart.Add(existingItem);
            }
            else
            {
                CartItemModel item = new CartItemModel()
                {
                    Product = SelectedProduct,
                    QuantityCart = ItemQuantity
                };
                Cart.Add(item);
            }
          
            SelectedProduct.QuantityStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
        
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;


                // Make sure something is selected
                // Make sure ther is an item quantity
                return output;
            }

        }
        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;


                return output;
            }

        }
        public void CheckOut()
        {

        }




    }
}
