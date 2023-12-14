using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services;
using TodoList.Services.Interfaces;

namespace TodoList.ViewModels
{
    public class AddTaskViewModel : BaseViewModel
    {
        public AddTaskViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
