using System;
using System.Collections.Generic;
using System.Linq;
using MvvmHelpers;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using AzureMobileClient.Helpers;
using MonkeyChat3.Data;
using MonkeyChat3.Models;
using MonkeyChat3.Strings;

namespace MonkeyChat3.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ICloudService _cloudService { get; }

        private IAppDataContext _dataContext { get; }

        public MainPageViewModel(INavigationService navigationService, IApplicationStore applicationStore, 
                                 IDeviceService deviceService, IAppDataContext dataContext, ICloudService cloudService)
            : base(navigationService, applicationStore, deviceService)
        {
            _cloudService = cloudService;
            _dataContext = dataContext;

            Title = Resources.MainPageTitle;
            TodoItems = new ObservableRangeCollection<TodoItem>();

            AddItemCommand = new DelegateCommand(OnAddItemCommandExecuted);
            DeleteItemCommand = new DelegateCommand<TodoItem>(OnDeleteItemCommandExecuted);
            TodoItemTappedCommand = new DelegateCommand<TodoItem>(OnTodoItemTappedCommandExecuted);
        }

        public ObservableRangeCollection<TodoItem> TodoItems { get; set; }

        public DelegateCommand AddItemCommand { get; }

        public DelegateCommand<TodoItem> DeleteItemCommand { get; }

        public DelegateCommand<TodoItem> TodoItemTappedCommand { get; }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            IsBusy = true;
            switch(parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
                    TodoItems.ReplaceRange(await _dataContext.TodoItems.ReadAllItemsAsync());
                    break;
                case NavigationMode.New:
                    await _cloudService.LoginAsync();
                    TodoItems.AddRange(await _dataContext.TodoItems.ReadAllItemsAsync());
                    break;
            }
            IsBusy = false;
        }

        private async void OnAddItemCommandExecuted() => 
            await _navigationService.NavigateAsync("TodoItemDetail", new NavigationParameters
            {
                { "new", true },
                { "todoItem", new TodoItem() }
            });

        private void OnDeleteItemCommandExecuted(TodoItem item) =>
            TodoItems.Remove(item);

        private async void OnTodoItemTappedCommandExecuted(TodoItem item) =>
            await _navigationService.NavigateAsync("TodoItemDetail", "todoItem", item);
    }
}
