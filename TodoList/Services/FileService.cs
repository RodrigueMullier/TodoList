using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Services.Interfaces;

namespace TodoList.Services
{
    public class FileService : IFileService
    {
        public async Task<T> ReadFile<T>(string filePath) where T : class
        {
            try
            {
                string jsonContent = await ReadFileAsync(filePath);

                return JsonConvert.DeserializeObject<T>(jsonContent)!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<string> ReadFileAsync(string filePath)
        {
            using FileStream sourceStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
            StringBuilder sb = new();

            byte[] buffer = new byte[0x1000];
            int numRead;
            while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                string text = Encoding.UTF8.GetString(buffer, 0, numRead);
                sb.Append(text);
            }

            return sb.ToString();
        }
    }
}
