using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SiteCarAsp.Services
{
    public interface IDataProvider<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T elem);
    }

    public abstract class FileDataProvider<T> : IDataProvider<T>
    {
        private readonly string filePath;
        private List<T> cache;

        protected FileDataProvider(string filePath)
        {
            this.filePath = filePath;
        }

        public async Task AddAsync(T elem)
        {
            await GetAllAsync();
            cache.Add(elem);
            await SaveDataToFile();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (cache != null)
                return cache;

            var directoryPath = Directory.GetParent(filePath).FullName;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            using var fs = new FileStream(filePath, FileMode.Open);
            return cache = (await JsonSerializer.DeserializeAsync<IEnumerable<T>>(fs)).ToList();
        }

        protected async Task SaveDataToFile()
        {
            if (cache == null)
                return;

            var directoryPath = Directory.GetParent(filePath).FullName;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            using var fs = new FileStream(filePath, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync(fs, cache);
        }
    }
}
