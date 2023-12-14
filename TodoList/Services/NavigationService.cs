using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services.Interfaces;
using TodoList.ViewModels;

namespace TodoList.Services
{
    public class NavigationService : BaseService, INavigationService
    {
        private readonly Func<Type, BaseViewModel> _viewModelFactory;
        public BaseViewModel CurrentView { get; private set; }

        public NavigationService(Func<Type, BaseViewModel> viewModelFactory) 
        {
            _viewModelFactory = viewModelFactory;
        }

        /// <summary>
        /// Navigation between pages
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void NavigateTo<T>() where T : BaseViewModel
        {
            BaseViewModel viewModel = _viewModelFactory.Invoke(typeof(T));
            CurrentView = viewModel;
        }
    }
}
