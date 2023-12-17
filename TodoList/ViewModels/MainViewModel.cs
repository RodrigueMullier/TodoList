using TodoList.Services.Interfaces;

namespace TodoList.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(ISession session, INavigationService navigationService) : base(session, navigationService)
        {
            NavigationService.NavigateTo<HomeViewModel>();
        }
    }
}
