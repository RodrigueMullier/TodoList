using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services;
using TodoList.Services.Interfaces;

namespace TodoList.ViewModels
{
    public abstract class BaseViewModel(INavigationService navigationService) : INotifyPropertyChanged
    {
        public INavigationService NavigationService { get; set; } = navigationService;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
    }
}