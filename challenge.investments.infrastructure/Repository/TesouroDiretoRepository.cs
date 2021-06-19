using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using challenge.investments.domain.Entities;
using challenge.investments.domain.Interfaces.Repository;
using challenge.investments.infrastructure.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace challenge.investments.infrastructure.Repository
{
    public class TesouroDiretoRepository : ITesouroDiretoRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        public TesouroDiretoRepository(HttpClient httpClient, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }
        public async Task<TesourosModel> Get()
        {
            try
            {
                if (_memoryCache.TryGetValue("tesouroData", out TesourosModel tesourosModel))
                    return tesourosModel;

                var response = await _httpClient.GetAsync(_configuration["TesouroUrl"]);

                if (response.IsSuccessStatusCode)
                {
                    var contentRead = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(contentRead))
                        return MemoryCacheHelper.CreateMemoryCache(_memoryCache, "tesouroData", JsonSerializer.Deserialize<TesourosModel>(contentRead));
                }
                return new TesourosModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
