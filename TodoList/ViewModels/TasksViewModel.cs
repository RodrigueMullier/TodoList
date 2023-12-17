using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using TodoList.Models;
using TodoList.Services.Interfaces;
using TodoList.Utils;
using TodoList.Utils.Enums;
using TodoList.Utils.Helpers;

namespace TodoList.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        private readonly IFileService _fileService;
        private ObservableCollection<Item> _items = [];
        public ObservableCollection<Item> DisplayedItems { get; set; } = [];
        public ObservableCollection<CategoryItem> Categories { get; set; } = [];
        public RelayCommand<ItemCategory> FilterCommand { get; set; }
        public RelayCommand NavigateToAddTaskCommand { get; set; }
        public AsyncRelayCommand<int> MarkAsFinishedCommand { get; set; }
        public RelayCommand<int> UpdateItemCommand { get; set; }
        public AsyncRelayCommand<int> DeleteItemCommand { get; set; }
        public TasksViewModel(ISession session, INavigationService navigationService, IFileService fileService) : base(session, navigationService)
        {
            _fileService = fileService;

            NavigateToAddTaskCommand = new RelayCommand(NavigationService.NavigateTo<AddTaskViewModel>);
            FilterCommand = new RelayCommand<ItemCategory>(OnFilter);
            MarkAsFinishedCommand = new AsyncRelayCommand<int>(OnMarkAsFinished);
            UpdateItemCommand = new RelayCommand<int>(OnUpdateItem);
            DeleteItemCommand = new AsyncRelayCommand<int>(OnDeleteItem);

            Categories = CategoryHelper.GetCategories();
        }
        protected override async Task OnPageLoaded(CancellationToken ct)
        {
            try
            {
                var items = await _fileService.ReadFile<ObservableCollection<Item>>(Constants.DATA_PATH);
                _items = new ObservableCollection<Item>(items.OrderByDescending(x => x.Id));
                DisplayedItems = new ObservableCollection<Item>(_items);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des données : {ex.Message}");
            }
        }

        /// <summary>
        /// Filter items
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        private void OnFilter(ItemCategory categoryName)
        {
            // if we select a category which is not selected yet
            if (!Categories.SingleOrDefault(f => f.Category == categoryName)!.IsSelected)
            {
                Categories.Where(c => c.IsSelected).ToList().ForEach(c => c.IsSelected = false);
                SetSelected(categoryName, true);

                var items = new ObservableCollection<Item>();
                foreach (var item in _items.Where(i => i.Category == categoryName))
                {
                    items.Add(item);
                }
                DisplayedItems = items;
            }
            else
            {
                SetSelected(categoryName, false);
                DisplayedItems = new ObservableCollection<Item>(_items);
            }
        }
        private void SetSelected(ItemCategory categoryName, bool isSelected)
        {
            CategoryItem category = Categories.Single(c => c.Category == categoryName);
            category.IsSelected = isSelected;
        }


        /// <summary>
        /// Mark an item as finished
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task OnMarkAsFinished(int id)
        {
            if (DisplayedItems.Any(i => i.Id == id))
            {
                Item item = DisplayedItems.Single(i => i.Id == id);
                item.MarkAsFinished();

                await _fileService.WriteFile(Constants.DATA_PATH, JsonConvert.SerializeObject(_items));
            }
        }

        /// <summary>
        /// Get the selected item and send it to being received by listeners
        /// </summary>
        /// <param name="id"></param>
        private void OnUpdateItem(int id)
        {
            if (DisplayedItems.Any(i => i.Id == id))
            {
                Item item = DisplayedItems.Single(i => i.Id == id);
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
            if (DisplayedItems.Any(i => i.Id == id))
            {
                Item item = DisplayedItems.Single(i => i.Id == id);
                _items.Remove(item);
                DisplayedItems.Remove(item);
                await _fileService.WriteFile(Constants.DATA_PATH, JsonConvert.SerializeObject(_items));
            }
        }

        public override void CleanUp()
        {
            base.CleanUp();

            Categories.Where(c => c.IsSelected).ToList().ForEach(c => c.IsSelected = false);
        }
    }
}