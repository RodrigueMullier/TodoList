using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services;
using TodoList.Services.Interfaces;
using TodoList.Tests.Services;
using TodoList.Utils.Enums;
using TodoList.ViewModels;

namespace TodoList.Tests.ViewModels
{
    [TestClass]
    public class TasksViewModelMock
    {
        readonly TasksViewModel _viewModel;
        readonly ISession _session = new Session();
        readonly INavigationService _navigationService = new NavigationService(null);
        readonly IFileService _fileService = new FileServiceMock();
        public TasksViewModelMock() 
        {
            _viewModel = new TasksViewModel(_session, _navigationService, _fileService);
            _viewModel.PageLoaded();
        }

        [TestMethod]
        [DataRow(ItemCategory.Loisirs)]
        public void OnFilterSuccessTest(ItemCategory itemCategory)
        {
            // count items for the filter category in the global list
            int nbItemsForCategory = _viewModel.DisplayedItems.Count(i => i.Category == itemCategory);

            // show only items for the filter category
            _viewModel.FilterCommand.Execute(itemCategory);

            // nbItemsForCategory has to be equal with the full list count items
            Assert.AreEqual(nbItemsForCategory, _viewModel.DisplayedItems.Count());

            // IsSelected for the filter category has to be true
            Assert.IsTrue(_viewModel.Categories.Single(c => c.Category == itemCategory).IsSelected);
        }

        [TestMethod]
        [DataRow(1)]
        public void OnMarkAsFinishedSuccessTest(int id)
        {
            // count items as finished
            int beforeCount = _viewModel.DisplayedItems.Count(i => i.IsFinished);

            // finish 1 item
            _viewModel.MarkAsFinishedCommand.Execute(id);

            // items as finished has to be equal with the beforeCount + 1 more item
            Assert.AreEqual(beforeCount + 1, _viewModel.DisplayedItems.Count(i => i.IsFinished));
        }

        [TestMethod]
        [DataRow(2)]
        public void OnDeleteItemSuccessTest(int id)
        {
            // count full list items
            int beforeCount = _viewModel.DisplayedItems.Count();

            // delete 1 item
            _viewModel.DeleteItemCommand.Execute(id);

            //items count has to be beforeCount - the deleted item
            Assert.AreEqual(beforeCount - 1, _viewModel.DisplayedItems.Count());
        }

        [TestMethod]
        [DataRow(4)]
        public void OnDeleteItemFailedTest(int id)
        {
            // count full list items
            int beforeCount = _viewModel.DisplayedItems.Count();

            // delete 1 item but failed as Id 4 not exists
            _viewModel.DeleteItemCommand.Execute(id);

            // count list has to be not changed
            Assert.AreEqual(beforeCount, _viewModel.DisplayedItems.Count());
        }
    }
}
