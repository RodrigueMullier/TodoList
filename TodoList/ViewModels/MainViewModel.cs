using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services;
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
