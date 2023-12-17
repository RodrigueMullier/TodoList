using TodoList.ViewModels;

namespace TodoList.Services.Interfaces
{
    public interface INavigationService
    {
        BaseViewModel CurrentView { get; }
        void NavigateTo<T>() where T : BaseViewModel;
    }
}
