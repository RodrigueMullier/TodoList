using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Services.Interfaces
{
    public interface IFileService
    {
        Task<T> ReadFile<T>(string relativePath) where T : class;
        Task<bool> WriteFile(string relativePath, string content);
    }
}
