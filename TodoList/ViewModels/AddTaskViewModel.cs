using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Services;
using TodoList.Services.Interfaces;
using TodoList.Utils;
using TodoList.Utils.Enums;
using TodoList.Utils.Helpers;

namespace TodoList.ViewModels
{
    public class AddTaskViewModel : BaseViewModel
    {
        private readonly IFileService _fileService;
        public string SubmitText { get; set; } = "";
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ItemCategory? SelectedCategory => Categories.Single(c => c.IsSelected == true)?.Category;
        public ObservableCollection<CategoryItem> Categories { get; set; } = [];
        public RelayCommand NavigateToTasksCommand { get; set; }
        public AsyncRelayCommand SubmitTaskCommand { get; set; }
        public RelayCommand<ItemCategory> SetCategoryCommand { get; set; }
        public AddTaskViewModel(ISession session, INavigationService navigationService, IFileService fileService) : base(session, navigationService)
        {
            _fileService = fileService;

            SubmitTaskCommand = new AsyncRelayCommand(OnSubmitTaskCommand);
            NavigateToTasksCommand = new RelayCommand(NavigationService.NavigateTo<TasksViewModel>);
            SetCategoryCommand = new RelayCommand<ItemCategory>(OnSetCategory);

            Categories = CategoryHelper.GetCategories();
        }

        protected override Task OnPageLoaded(CancellationToken ct)
        {
            if (Session.SelectedItem != null)
            {
                SubmitText = "Modifier la tâche";
                Title = Session.SelectedItem.Title;
                Description = Session.SelectedItem.Description;
                OnSetCategory(Session.SelectedItem.Category);
            }
            else
            {
                SubmitText = "Ajouter une tâche";
            }

            return base.OnPageLoaded(ct);
        }



        /// <summary>
        /// Set a filter
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        private void OnSetCategory(ItemCategory categoryName)
        {
            Categories.Where(c => c.IsSelected).ToList().ForEach(c => c.IsSelected = false);
            Categories.Single(c => c.Category == categoryName).IsSelected = true;
        }

        /// <summary>
        /// Form validation
        /// </summary>
        /// <returns></returns>
        private async Task OnSubmitTaskCommand()
        {
            try
            {
                ObservableCollection<Item> items = await _fileService.ReadFile<ObservableCollection<Item>>(Constants.DATA_PATH);

                // if new task
                if(Session.SelectedItem == null) 
                {
                    items ??= [];
                    items.Add(new Item()
                    {
                        Id = items.Count == 0 ? 1 : items.OrderByDescending(x => x.Id).Select(x => x.Id).First() + 1,
                        Title = Title!,
                        Description = Description!,
                        Category = SelectedCategory!.Value,
                    });
                }
                // if update task
                else
                {
                    Item item = items.Single(i => i.Id ==  Session.SelectedItem.Id);
                    item.Title = Title!;
                    item.Description = Description!;
                    item.Category = SelectedCategory!.Value;
                }

                bool result = await _fileService.WriteFile(Constants.DATA_PATH, JsonConvert.SerializeObject(items));

                if(result)
                {
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des données : {ex.Message}");
            }
        }

        private void ClearInputs()
        {
            Title = string.Empty;
            Description = string.Empty;
            Categories.Where(c => c.IsSelected).ToList().ForEach(c => c.IsSelected = false);
        }

        public override void CleanUp()
        {
            base.CleanUp();

            Session.SelectedItem = null;
            ClearInputs();
        }
    }
}
