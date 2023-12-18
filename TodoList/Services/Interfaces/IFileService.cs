namespace TodoList.Services.Interfaces
{
    public interface IFileService
    {
        Task<T> ReadFile<T>(string relativePath) where T : class;
        Task<bool> WriteFile(string relativePath, string content);
    }
}
