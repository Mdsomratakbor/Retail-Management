﻿using Caliburn.Micro;
using RMDekstopUI.Library.ViewModels;
using RMDesktopUI.LIbrary.Api;
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

        public   SalesViewModel(IProductEndPoint productEndPoint)
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

        private BindingList<string> _cart;

        public BindingList<string> Cart
        {
            get { return _cart; }
            set {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }


        private int _itemQuantity;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }


        public string SubTotal
        {
            get {  
                // TODO - Replace with Calculation
                return "$0.00";
            }
        }
        public string Tax
        {
            get
            {
                // TODO - Replace with Calculation
                return "$0.00";
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
                return output;
            }

        }
        public void AddToCart()
        {

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
