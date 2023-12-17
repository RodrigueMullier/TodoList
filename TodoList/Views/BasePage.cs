using System.Windows;
using System.Windows.Controls;
using TodoList.ViewModels;

namespace TodoList.Views
{
    public abstract class BasePage : Page
    {
        private BaseViewModel? _viewModel;
        public BasePage() 
        {
            Loaded += Page_Loaded;
            Unloaded += Page_Unloaded;
        }
        protected void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as BaseViewModel;
            _viewModel?.PageLoaded();
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _viewModel?.CleanUp();
            _viewModel = null;
        }
    }
}