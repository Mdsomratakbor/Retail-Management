using Caliburn.Micro;
using RMDesktopUI.LIbrary.Api;
using RMDesktopUI.LIbrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMDekstopUI.ViewModels
{
    class UserDisplayViewModel: Screen
    {
        private readonly StatusInfoViewModel _status;
        private readonly IWindowManager _window;
        private readonly IUserEndPoint _userEndPoint;
        BindingList<UserModel> _users;
        public BindingList<UserModel> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                NotifyOfPropertyChange(()=> Users);
            }
        }

        private UserModel _selectedUser;
        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set { 
                _selectedUser = value;
                SelectedUserName = value.Email;
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        private string _selecteduserName;
        public string SelectedUserName
        {
            get { return _selecteduserName; }
            set
            {
                _selecteduserName = value;
                NotifyOfPropertyChange(() => SelectedUserName);
            }
        }

        private BindingList<string> _selectedUserRoles = new BindingList<string>();

        public  BindingList<string> SelectedUserRoles
        {
            get { return _selectedUserRoles; }
            set { 
                _selectedUserRoles = value;
            }
        }



        public UserDisplayViewModel(StatusInfoViewModel status, IWindowManager window, IUserEndPoint userEndPoint)
        {
            this._status = status;
            this._window = window;
            this._userEndPoint = userEndPoint;
        }
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadUsers();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    _status.UpdateMessage("Unauthorized Access", "You do not have permission to interact with the sales Form.");
                    await _window.ShowDialogAsync(_status, null, settings);
                }
                else
                {
                    _status.UpdateMessage("Fatal Exception", ex.Message);
                    await _window.ShowDialogAsync(_status, null, settings);
                }

                await TryCloseAsync();
            }

        }
        private async Task  LoadUsers()
        {
            var userList = await _userEndPoint.GetAll();
            Users = new BindingList<UserModel>(userList);
        }
    }
}
