using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IConfigurationService
    {
        Task<T> GetConfigValueAsync<T>(string key);
        Task<bool> SetConfigValueAsync<T>(string key, T value);
        Task<Dictionary<string, object>> GetAllConfigsAsync();
        Task<bool> ResetConfigToDefaultAsync(string key);
    }
}
