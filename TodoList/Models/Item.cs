using TodoList.Utils.Enums;

namespace TodoList.Models
{
    public class Item
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required ItemStatus Status { get; set; } = ItemStatus.Default;
    }
}