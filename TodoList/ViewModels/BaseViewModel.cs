using System.ComponentModel;
using TodoList.Services.Interfaces;

namespace TodoList.ViewModels
{
    public abstract class BaseViewModel(ISession session, INavigationService navigationService) : INotifyPropertyChanged
    {
        public INavigationService NavigationService { get; set; } = navigationService;
        public ISession Session { get; set; } = session;

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
            catch(Exception)
            {
                
            }
            finally 
            { 
                IsLoading = false; 
            }
        }
        protected virtual void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual void CleanUp() { }
    }
}