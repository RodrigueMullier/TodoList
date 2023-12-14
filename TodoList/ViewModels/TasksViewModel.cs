using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services;
using TodoList.Services.Interfaces;

namespace TodoList.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        public TasksViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
