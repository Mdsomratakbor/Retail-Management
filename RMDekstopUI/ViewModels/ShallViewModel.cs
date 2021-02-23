using Caliburn.Micro;
using RMDekstopUI.EventsModels;
using RMDesktopUI.LIbrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RMDekstopUI.ViewModels
{
    public class ShallViewModel: Conductor<object>, IHandle<LogOnEventModel>
    {
     
        private readonly IEventAggregator _events;
        private SalesViewModel _salesVM;
        private SimpleContainer _container;
        private ILoggedInUserModel _user;
        public  ShallViewModel( IEventAggregator events, SalesViewModel salesVM, SimpleContainer container, ILoggedInUserModel user)
        {
            _events = events;
            _user = user;
              _salesVM = salesVM;
            _container = container;
            _events.Subscribe(this);
            ActivateItemAsync(IoC.Get<LoginViewModel>());

        }
        public async Task  ExitApplication()
        {
           await TryCloseAsync();
        }
        public async Task  LogOut()
        {
            _user.LogOffUser();
           await  ActivateItemAsync(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
        public bool IsLoggedIn
        {
            get
            {
                bool output = false;
                if (string.IsNullOrWhiteSpace(_user.Token) == false)
                {
                    output = true;
                }
                return output;
            }

        }

        public async Task HandleAsync(LogOnEventModel message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_salesVM);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
