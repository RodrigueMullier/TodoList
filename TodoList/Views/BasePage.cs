using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TodoList.ViewModels;

namespace TodoList.Views
{
    public abstract class BasePage : Page
    {
        protected void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as BaseViewModel;
            viewModel?.PageLoaded();
        }
    }
}