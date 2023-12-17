using TodoList.Models;
using TodoList.Services.Interfaces;

namespace TodoList.Services
{
    public class Session : ISession
    {
        public Item? SelectedItem { get; set; }
    }
}
