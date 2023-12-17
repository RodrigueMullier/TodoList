using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TodoList.Utils.Enums;

namespace TodoList.Models
{
    public class CategoryItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ItemCategory Category { get; set; }
        public string HexaColor { get; set; }
        public bool IsSelected { get; set; }
    }
}
