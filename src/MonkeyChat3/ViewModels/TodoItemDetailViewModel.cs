using System;
using System.Collections.Generic;
using System.Linq;
using MvvmHelpers;
using Prism.AppModel;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using MonkeyChat3.Data;
using MonkeyChat3.Models;
using MonkeyChat3.Strings;

namespace MonkeyChat3.ViewModels
{
    public class TodoItemDetailViewModel : ViewModelBase
    {
        private IAppDataContext _dataContext { get; }

        public TodoItemDetailViewModel(INavigationService navigationService, IApplicationStore applicationStore, 
                                       IDeviceService deviceService, IAppDataContext dataContext)
            : base(navigationService, applicationStore, deviceService)
        {
            _dataContext = dataContext;

            Title = Resources.TodoItemDetailTitle;
            SaveCommand = new DelegateCommand(OnSaveCommandExecuted);
        }

        public TodoItem Model { get; set; }

        public DelegateCommand SaveCommand { get; }

        private bool _isNew;

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            _isNew = parameters.GetValue<bool>("new");
            Model = parameters.GetValue<TodoItem>("todoItem");
        }

        private async void OnSaveCommandExecuted()
        {
            if(_isNew)
            {
                await _dataContext.TodoItems.CreateItemAsync(Model);
            }
            else
            {
                await _dataContext.TodoItems.UpdateItemAsync(Model);
            }

            await _navigationService.GoBackAsync();
        }
    }
}