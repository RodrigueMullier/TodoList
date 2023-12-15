using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TodoList.Utils.Enums;

namespace TodoList.Models
{
    public class Item
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ItemStatus Status { get; set; } = ItemStatus.Default;
        public bool IsFinished => Status == ItemStatus.Finished;

        public void MarkAsFinished()
        {
            Status = ItemStatus.Finished;
        }
    }
}