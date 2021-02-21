using Caliburn.Micro;
using RMDekstopUI.EventsModels;
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

        public  ShallViewModel( IEventAggregator events, SalesViewModel salesVM, SimpleContainer container)
        {
            _events = events;
          
            _salesVM = salesVM;
            _container = container;
            _events.Subscribe(this);
            ActivateItemAsync(IoC.Get<LoginViewModel>());

        }

        public async Task HandleAsync(LogOnEventModel message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_salesVM)
     ;
        }
    }
}
