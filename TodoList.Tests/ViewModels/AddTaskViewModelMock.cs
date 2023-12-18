using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Services;
using TodoList.Services.Interfaces;
using TodoList.Tests.Services;
using TodoList.Utils.Enums;
using TodoList.ViewModels;

namespace TodoList.Tests.ViewModels
{
    [TestClass]
    public class AddTaskViewModelMock
    {
        readonly AddTaskViewModel _viewModel;
        readonly ISession _session = new Session();
        readonly INavigationService _navigationService = new NavigationService(null);
        readonly IFileService _fileService = new FileServiceMock();
        public AddTaskViewModelMock()
        {
            _viewModel = new AddTaskViewModel(_session, _navigationService, _fileService);
            _viewModel.PageLoaded();
        }

        [TestMethod]
        public async Task OnSubmitAddTaskSuccessTest()
        {
            ObservableCollection<Item> items = await _fileService.ReadFile<ObservableCollection<Item>>(null);

            _session.SelectedItem = null;
            _viewModel.Title = "New item";
            _viewModel.Description = "New description";
            _viewModel.SetCategoryCommand.Execute(ItemCategory.Santé);

            _viewModel.SubmitTaskCommand.Execute(null);

            ObservableCollection<Item> newItems = await _fileService.ReadFile<ObservableCollection<Item>>(null);

            // confirmation label check
            Assert.AreEqual(_viewModel.ConfirmLabelValidation, "Tâche ajoutée avec succés");

            // IsSelected for the filter category has to be true
            Assert.AreEqual(items.Count + 1, newItems.Count);
        }

        [TestMethod]
        public async Task OnSubmitUpdateTaskSuccessTest()
        {
            ObservableCollection<Item> items = await _fileService.ReadFile<ObservableCollection<Item>>(null);
            _session.SelectedItem = items.First();

            _viewModel.Title = "New item title";
            _viewModel.Description = "New item description";
            _viewModel.SetCategoryCommand.Execute(ItemCategory.Autre);

            _viewModel.SubmitTaskCommand.Execute(null);

            ObservableCollection<Item> newItems = await _fileService.ReadFile<ObservableCollection<Item>>(null);

            // confirmation label check
            Assert.AreEqual(_viewModel.ConfirmLabelValidation, "Tâche modifiée avec succés");

            // check properties updated item
            Assert.AreEqual("New item title", newItems.First().Title);
            Assert.AreEqual("New item description", newItems.First().Description);
            Assert.AreEqual(ItemCategory.Autre, newItems.First().Category);

            // check count list, has to be equals
            Assert.AreEqual(items.Count(), newItems.Count());
        }
    }
}
