using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
        public RelayCommand<int> UpdateItemCommand { get; set; }
        public AsyncRelayCommand<int> DeleteItemCommand { get; set; }
        public TasksViewModel(ISession session, INavigationService navigationService, IFileService fileService) : base(session, navigationService)
        {
            _fileService = fileService;

            NavigateToAddTaskCommand = new RelayCommand(NavigationService.NavigateTo<AddTaskViewModel>);
            MarkAsFinishedCommand = new AsyncRelayCommand<int>(OnMarkAsFinished);
            UpdateItemCommand = new RelayCommand<int>(OnUpdateItem);
            DeleteItemCommand = new AsyncRelayCommand<int>(OnDeleteItem);
        }
        protected override async Task OnPageLoaded(CancellationToken ct)
        {
            try
            {
                var items = await _fileService.ReadFile<ObservableCollection<Item>>(Constants.DATA_PATH);
                Items = new ObservableCollection<Item>(items.OrderByDescending(x => x.Id));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des données : {ex.Message}");
            }
        }

        /// <summary>
        /// Mark an item as finished
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task OnMarkAsFinished(int id)
        {
            if (Items.Any(i => i.Id == id))
            {
                Item item = Items.Single(i => i.Id == id);
                item.MarkAsFinished();
                await _fileService.WriteFile(Constants.DATA_PATH, JsonConvert.SerializeObject(Items));
            }
        }

        /// <summary>
        /// Get the selected item and send it to being received by listeners
        /// </summary>
        /// <param name="id"></param>
        private void OnUpdateItem(int id)
        {
            if(Items.Any(i => i.Id == id))
            {
                Item item = Items.Single(i => i.Id == id);
                Session.SelectedItem = item;
                NavigationService.NavigateTo<AddTaskViewModel>();
            }
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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