// Services/ArticuloService.cs
using BlazorCrud.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace BlazorCrud.Services
{
    public class ArticuloService
    {
        private readonly HttpClient _httpClient;

        public ArticuloService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MArticulo>> GetArticulos()
        {
            var response = await _httpClient.GetAsync("art/listado");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<List<MArticulo>>();
                return content ?? new List<MArticulo>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = JsonSerializer.Deserialize<JsonElement>(errorContent)
                               .GetProperty("message").GetString() ?? "Error desconocido";
                throw new HttpRequestException(errorMessage);
            }

        }

        public async Task DeleteArticulo(int codnum)
        {
            var response = await _httpClient.DeleteAsync($"art/deshabilitar?codnum={codnum}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = JsonSerializer.Deserialize<JsonElement>(errorContent)
                               .GetProperty("message").GetString() ?? "Error desconocido";
                throw new HttpRequestException(errorMessage);
            }
        }

        public async Task<MArticulo> GetArticuloById(int codnum)
        {
            var response = await _httpClient.GetAsync($"art/buscarId?codnum={codnum}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MArticulo>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = JsonSerializer.Deserialize<JsonElement>(errorContent)
                               .GetProperty("message").GetString() ?? "Error desconocido";
                throw new HttpRequestException(errorMessage);
            }
        }

        public async Task UpdateArticulo(MArticulo articulo)
        {
            var response = await _httpClient.PutAsJsonAsync($"art/ModArt", articulo);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = JsonSerializer.Deserialize<JsonElement>(errorContent)
                               .GetProperty("message").GetString() ?? "Error desconocido";
                throw new HttpRequestException(errorMessage);
            }
        }

        public async Task CreateArticulo(MArticulo articulo)
        {
            var response = await _httpClient.PostAsJsonAsync($"art/AggArt", articulo);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = JsonSerializer.Deserialize<JsonElement>(errorContent)
                               .GetProperty("message").GetString() ?? "Error desconocido";
                throw new HttpRequestException(errorMessage);
            }
        }
    }
}
