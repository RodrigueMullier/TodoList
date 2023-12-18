using Newtonsoft.Json;
using System.IO;
using System.Text;
using TodoList.Services.Interfaces;

namespace TodoList.Services
{
    public class FileService : IFileService
    {
        public async Task<T> ReadFile<T>(string relativePath) where T : class
        {
            try
            {
                string jsonContent = await ReadFileAsync(GetAbsolutePath(relativePath));

                return JsonConvert.DeserializeObject<T>(jsonContent)!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task<string> ReadFileAsync(string absolutePath)
        {
            using FileStream sourceStream = new(absolutePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
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
        public async Task<bool> WriteFile(string relativePath, string content)
        {
            try
            {
                await WriteTextAsync(GetAbsolutePath(relativePath), content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private async Task WriteTextAsync(string absolutePath, string text)
        {
            byte[] encodedText = Encoding.UTF8.GetBytes(text);

            using var sourceStream =
                new FileStream(
                    absolutePath,
                    FileMode.Create, FileAccess.Write, FileShare.None,
                    bufferSize: 4096, useAsync: true);

            await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
        }

        private string GetAbsolutePath(string relativePath)
        {
            string relativePathWithPrefix = @"..\..\..\" + relativePath;
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(currentDirectory, relativePathWithPrefix);
            return Path.GetFullPath(fullPath);
        }
    }
}
