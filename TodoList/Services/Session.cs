using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Services.Interfaces;

namespace TodoList.Services
{
    public class Session : ISession
    {
        public Item? SelectedItem { get; set; }
    }
}
