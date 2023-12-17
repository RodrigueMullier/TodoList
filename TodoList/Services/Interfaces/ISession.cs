using TodoList.Models;

namespace TodoList.Services.Interfaces
{
    public interface ISession
    {
        Item? SelectedItem { get; set; }
    }
}
