using Caliburn.Micro;
using RMDekstopUI.EventsModels;
using RMDesktopUI.LIbrary.Api;
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

        private SimpleContainer _container;
        private ILoggedInUserModel _user;
        private IAPIHelper _apiHelper;
        public  ShallViewModel( IEventAggregator events,  SimpleContainer container, ILoggedInUserModel user, IAPIHelper apiHelper)
        {
            _events = events;
            _user = user;
            _apiHelper = apiHelper;
            _container = container;
            _events.SubscribeOnPublishedThread(this);
            ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());

        }
        public async Task  ExitApplication()
        {
           await TryCloseAsync();
        }
        public async Task UserManagement()
        {
            await ActivateItemAsync(IoC.Get<UserDisplayViewModel>(), new CancellationToken());
        }
        public async Task  LogOut()
        {
            _user.ResetUserModel();
            _apiHelper.LogOffUser();
           await  ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
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
            await ActivateItemAsync(IoC.Get<SalesViewModel>(), cancellationToken);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
