using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Services.Interfaces
{
    public interface ISession
    {
        Item? SelectedItem { get; set; }
    }
}
