using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDekstopUI.ViewModels
{
    public class ShallViewModel: Conductor<Object>
    {
        private LoginViewModel _loginVM;
        public  ShallViewModel(LoginViewModel loginVM)
        {
            _loginVM = loginVM;
             ActivateItemAsync (_loginVM);
        }
    }
}
