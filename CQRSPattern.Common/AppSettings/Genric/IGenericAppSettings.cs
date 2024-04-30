namespace CQRSPattern.Common.AppSettings.Genric;

public interface IGenericAppSettings<T>
{
    T GetAppSettings();
    string GetAppSettingValue(string key);
}