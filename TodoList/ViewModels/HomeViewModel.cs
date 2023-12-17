using CommunityToolkit.Mvvm.Input;
using TodoList.Services.Interfaces;

namespace TodoList.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public RelayCommand NavigateToTasksCommand { get; set; }
        public HomeViewModel(ISession session, INavigationService navigationService) : base(session, navigationService)
        {
            NavigateToTasksCommand = new RelayCommand(NavigationService.NavigateTo<TasksViewModel>);
        }
    }
}