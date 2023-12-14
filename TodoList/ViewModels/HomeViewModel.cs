using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services.Interfaces;

namespace TodoList.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public RelayCommand NavigateToTasksCommand { get; set; }
        public HomeViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigateToTasksCommand = new RelayCommand(NavigationService.NavigateTo<TasksViewModel>);
        }
    }
}