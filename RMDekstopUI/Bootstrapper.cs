﻿using AutoMapper;
using Caliburn.Micro;
using RMDekstopUI.Helper;
using RMDekstopUI.Library.ViewModels;
using RMDekstopUI.Models;
using RMDekstopUI.ViewModels;
using RMDekstopUI.Views;
using RMDesktopUI.LIbrary.Api;
using RMDesktopUI.LIbrary.Helpers;
using RMDesktopUI.LIbrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RMDekstopUI
{
    public class Bootstrapper :BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper() {

            Initialize();
            ConventionManager.AddElementConvention<PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
            "Password",
            "PasswordChanged");
        }
        private IMapper ConfigurAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductModel, ProductDisplayModel>();
                cfg.CreateMap<CartItemModel, CartItemDisplayModel>();
            });
            var output = config.CreateMapper();
            return output;
        }
        protected override void Configure()
        {
            

            _container.Instance(ConfigurAutoMapper());
            _container.Instance(_container)
                .PerRequest<IProductEndPoint, ProductEndPoint>()
                .PerRequest<IUserEndPoint, UserEndPoint>()
                .PerRequest<ISaleEndPoint, SaleEndPoint>();

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IAPIHelper, APIHelper>()
                .Singleton<ILoggedInUserModel, LoggedInUserModel>()
                .Singleton<IConfigHelper, ConfigHelper>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShallViewModel>();
        }
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

    }
}
