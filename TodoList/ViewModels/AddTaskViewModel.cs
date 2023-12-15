using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Services;
using TodoList.Services.Interfaces;
using TodoList.Utils;

namespace TodoList.ViewModels
{
    public class AddTaskViewModel : BaseViewModel
    {
        private readonly IFileService _fileService;
        public string Title { get; set; }
        public string Description { get; set; }

        public RelayCommand NavigateToTasksCommand { get; set; }
        public AsyncRelayCommand AddTaskCommand { get; set; }
        public AddTaskViewModel(INavigationService navigationService, IFileService fileService) : base(navigationService)
        {
            _fileService = fileService;
            AddTaskCommand = new AsyncRelayCommand(OnAddTaskCommand);
            NavigateToTasksCommand = new RelayCommand(NavigationService.NavigateTo<TasksViewModel>);
        }
        private async Task OnAddTaskCommand()
        {
            try
            {
                List<Item> items = await _fileService.ReadFile<List<Item>>(Constants.DATA_PATH);
                items.Add(new Item()
                {
                    Id = items.OrderByDescending(x => x.Id).Select(x => x.Id).First() + 1,
                    Title = Title,
                    Description = Description,
                });

                bool result = await _fileService.WriteFile(Constants.DATA_PATH, JsonConvert.SerializeObject(items));

                if(result)
                {
                    Title = string.Empty;
                    Description = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des données : {ex.Message}");
            }


            Console.WriteLine(Title);
            Console.WriteLine(Description);
        }
    }
}
