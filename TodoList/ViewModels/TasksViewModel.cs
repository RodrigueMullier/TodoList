using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Services;
using TodoList.Services.Interfaces;
using TodoList.Utils;
using TodoList.Utils.Enums;

namespace TodoList.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        private readonly IFileService _fileService;
        public ObservableCollection<Item> Items { get; set; } = [];
        public RelayCommand NavigateToAddTaskCommand { get; set; }
        public AsyncRelayCommand<int> MarkAsFinishedCommand { get; set; }
        public AsyncRelayCommand<int> DeleteItemCommand { get; set; }

        public TasksViewModel(INavigationService navigationService, IFileService fileService) : base(navigationService)
        {
            _fileService = fileService;
            NavigateToAddTaskCommand = new RelayCommand(NavigationService.NavigateTo<AddTaskViewModel>);
            MarkAsFinishedCommand = new AsyncRelayCommand<int>(OnMarkAsFinished);
            DeleteItemCommand = new AsyncRelayCommand<int>(OnDeleteItem);
        }
        protected override async Task OnPageLoaded(CancellationToken ct)
        {
            try
            {
                Items = await _fileService.ReadFile<ObservableCollection<Item>>(Constants.DATA_PATH);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des données : {ex.Message}");
            }
        }

        private async Task OnMarkAsFinished(int id)
        {
            if (Items.Any(i => i.Id == id))
            {
                Item item = Items.Single(i => i.Id == id);
                item.MarkAsFinished();
                await _fileService.WriteFile(Constants.DATA_PATH, JsonConvert.SerializeObject(Items));
            }
        }
        private async Task OnDeleteItem(int id)
        {
            if(Items.Any(i => i.Id == id))
            {
                Items.Remove(Items.Single(i => i.Id == id));
                await _fileService.WriteFile(Constants.DATA_PATH, JsonConvert.SerializeObject(Items));
            }
        }
    }
}