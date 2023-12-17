﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Utils.Enums;

namespace TodoList.Utils.Helpers
{
    public class CategoryHelper
    {
        public static ObservableCollection<CategoryItem> GetCategories()
        {
            ObservableCollection<CategoryItem> categories = [];
            foreach (var name in Enum.GetValues<ItemCategory>())
            {
                categories.Add(new CategoryItem()
                {
                    Category = name,
                    HexaColor = Constants.CATEGORY_COLORS[name],
                    IsSelected = false
                });
            }

            return categories;
        }
    }
}
