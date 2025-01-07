namespace ImFreeFromApp.Ui.Services.StorageService;

public interface IStorageService
{
    Task SaveDataAsync<T>(string key, T data);
    Task<T> LoadDataAsync<T>(string key);
}