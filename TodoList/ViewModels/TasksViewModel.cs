using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Services;
using TodoList.Services.Interfaces;
using TodoList.Utils.Enums;

namespace TodoList.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        private readonly IFileService _fileService;
        public List<Item> Items { get; set; } = [];
        public RelayCommand NavigateToAddTaskCommand { get; set; }

        public TasksViewModel(INavigationService navigationService, IFileService fileService) : base(navigationService)
        {
            _fileService = fileService;
            NavigateToAddTaskCommand = new RelayCommand(NavigationService.NavigateTo<AddTaskViewModel>);
        }
        protected override async Task OnPageLoaded(CancellationToken ct)
        {
            try
            {
                Items = await _fileService.ReadFile<List<Item>>("Data/tasks.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des données : {ex.Message}");
            }
        }
    }
}