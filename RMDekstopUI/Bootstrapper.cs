using Caliburn.Micro;
using RMDekstopUI.ViewModels;
using RMDekstopUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMDekstopUI
{
    public class Bootstrapper :BootstrapperBase
    {
        public Bootstrapper() {

            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShallViewModel>();
        }
    }
}
