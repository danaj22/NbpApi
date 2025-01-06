using Azure;
using CurrencyAppApi.Exceptions;

namespace CurrencyAppApi.Services
{
    public abstract class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiClient> _logger;

        protected ApiClient(HttpClient httpClient, ILogger<ApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        protected async Task<T?> GetAsync<T>(string relativeUri, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{relativeUri}", cancellationToken);
            
            if(response.StatusCode is System.Net.HttpStatusCode.NotFound)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogWarning(errorMessage);
                return default;
            }
            else if(!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError(errorMessage);
                throw new NbpException(errorMessage);
            }

            return await response.Content.ReadFromJsonAsync<T>(cancellationToken);
            
        }
    }
}
