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
        public bool IsLoading { get; set; }

        protected virtual Task OnPageLoaded(CancellationToken ct)
        {
            return Task.CompletedTask;
        }
        public async Task PageLoaded()
        {
            IsLoading = true;

            try
            {
                using CancellationTokenSource cts = new(10000);
                await OnPageLoaded(cts.Token);
            }
            catch(Exception ex)
            {
                
            }
            finally 
            { 
                IsLoading = false; 
            }
        }
        protected void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
    }
}