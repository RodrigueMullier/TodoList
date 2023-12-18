using System.ComponentModel;
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
