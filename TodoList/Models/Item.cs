using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using TodoList.Utils.Enums;

namespace TodoList.Models
{
    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ItemStatus Status { get; set; } = ItemStatus.Default;

        [JsonConverter(typeof(StringEnumConverter))]
        public ItemCategory Category { get; set; }

        [JsonIgnore]
        public bool IsFinished => Status == ItemStatus.Finished;
        public void MarkAsFinished()
        {
            Status = ItemStatus.Finished;
        }
    }
}