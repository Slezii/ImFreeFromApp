using System.Text.Json;
using ImFreeFromApp.Ui.Exceptions;

namespace ImFreeFromApp.Ui.Services.StorageService;

public class LocalStorageService : IStorageService
{
    private readonly string _baseStoragePath = FileSystem.AppDataDirectory;

    private string GetFilePath(string key)
    {
        var validFileName = string.Join("_", key.Split(Path.GetInvalidFileNameChars()));
        return Path.Combine(_baseStoragePath, $"{validFileName}.json");
    }

    public async Task SaveDataAsync<T>(string key, T data)
    {
        try
        {
            var filePath = GetFilePath(key);
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            await File.WriteAllTextAsync(filePath, json);
        }
        catch (Exception)
        {
            throw new WriteToLocalStorageException();
        }
    }

    public async Task<T> LoadDataAsync<T>(string key)
    {
        try
        {
            var filePath = GetFilePath(key);
            if (!File.Exists(filePath))
                return default;

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (Exception)
        {
            throw new ReadFromLocalStorageException();
        }
    }
}