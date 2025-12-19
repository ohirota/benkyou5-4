using SharedModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace benkyou5_4.Services
{
    public class EmployeeApiService
    {
        private readonly HttpClient _client = new HttpClient();

        public EmployeeApiService()
        {
            // APIのポート番号は自分の環境に合わせてください
            _client.BaseAddress = new System.Uri("https://localhost:7122/");
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<List<Employee>>("api/employee");
        }

        public async Task AddAsync(Employee emp)
        {
            await _client.PostAsJsonAsync("api/employee", emp);
        }

        // 追加分：更新
        public async Task UpdateAsync(Employee emp)
        {
            await _client.PutAsJsonAsync($"api/employee/{emp.Id}", emp);
        }

        // 追加分：削除
        public async Task DeleteAsync(int id)
        {
            await _client.DeleteAsync($"api/employee/{id}");
        }
    }
}