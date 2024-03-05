using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSPattern.Common.AppSettings.Genric;

public class GenericAppSettings<T> : IGenericAppSettings<T>
{
    private readonly IConfiguration _configuration;

    public GenericAppSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public T GetAppSettings()
    {
        return _configuration.GetSection(typeof(T).Name).Get<T>();
    }

    public string GetAppSettingValue(string key)
    {
        return _configuration[key];
    }
}
