using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using TodoList;
using TodoList.Models;
using TodoList.Services.Interfaces;
using TodoList.Utils.Enums;

namespace TodoList.Tests.Services
{
    public class FileServiceMock : IFileService
    {
        ObservableCollection<Item> _items = new ObservableCollection<Item>
        {
            new() { Id = 1, Title = "Item 1", Description = "Description 1", Status = ItemStatus.Default, Category = ItemCategory.Loisirs },
            new() { Id = 2, Title = "Item 2", Description = "Description 2", Status = ItemStatus.Default, Category = ItemCategory.Alimentation },
            new() { Id = 3, Title = "Item 3", Description = "Description 3", Status = ItemStatus.Default, Category = ItemCategory.Travail }
        };
        public async Task<T> ReadFile<T>(string relativePath) where T : class
        {
            string jsonContent = JsonConvert.SerializeObject(_items);

            return JsonConvert.DeserializeObject<T>(jsonContent)!;
        }

        public async Task<bool> WriteFile(string relativePath, string content)
        {
            ObservableCollection<Item> items = JsonConvert.DeserializeObject<ObservableCollection<Item>>(content)!;
            _items = items;

            return true;
        }
    }
}
