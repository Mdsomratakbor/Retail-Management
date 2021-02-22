using Caliburn.Micro;
using RMDekstopUI.Library.ViewModels;
using RMDesktopUI.LIbrary.Api;
using RMDesktopUI.LIbrary.Helpers;
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
        private IConfigHelper _configHelper;
        private ISaleEndPoint _saleEndPoint;
        public SalesViewModel(IProductEndPoint productEndPoint, IConfigHelper configHelper, ISaleEndPoint saleEndPoint)
        {
            _productEndPoint = productEndPoint;
            _configHelper = configHelper;
            _saleEndPoint = saleEndPoint;
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

        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

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
                return CalculateSubTotal().ToString("C");
            }
        }

        private decimal CalculateSubTotal()
        {
            decimal subTotal = 0;
            foreach (var item in Cart)
            {
                subTotal += item.Product.RetailPrice * item.QuantityCart;
            }
            return subTotal;
        }
        private decimal CalculateTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = _configHelper.GetTaxRate()/100;
            //    foreach (var item in Cart)
            //   {
            //      if (item.Product.IsTaxable)
            //     {
            // taxAmount += (item.Product.RetailPrice * item.QuantityCart * taxRate);
            //    }

            //  }
            taxAmount += Cart.Where(x => x.Product.IsTaxable)
                .Sum(x => x.Product.RetailPrice * x.QuantityCart * taxRate);
            return taxAmount;
        }
        public string Tax
        {
            get
            { 
                return CalculateTax().ToString("C");
            }
        }
        
        public string Total
        {
            get
            {
                decimal total = CalculateSubTotal() + CalculateTax();
                return total.ToString("C");
            }
        }
        public bool CanAddToCart
        {
            get
            {
                bool output = false;
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
            if (existingItem != null)
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
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);

        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;
                return output;
            }

        }
        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;
                if (Cart.Count > 0)
                {
                    output = true;
                }

                return output;
            }

        }
        public async Task  CheckOut()
        {
            // Create a SalesModel and post to the API
            SaleModel sale = new SaleModel();
            foreach (var item in Cart)
            {
                sale.SalesDetails.Add(new SaleDetailModel
                {
                    ProductId = item.Product.Id,
                    Quantity = item.QuantityCart
                });
            }
            await _saleEndPoint.PostSale(sale);
        }




    }
}
