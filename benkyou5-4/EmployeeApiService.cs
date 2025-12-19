using SharedModels; // Employee クラスを使うため
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using benkyou5_4.Services;



namespace benkyou5_4.Services
{
    public class EmployeeApiService
    {
        private readonly HttpClient _client;

        public EmployeeApiService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7122/"); // Web API のURL
        }

        // 一覧取得
        public async Task<List<Employee>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<List<Employee>>("api/employee");
        }

        // 追加
        public async Task<Employee> AddAsync(Employee emp)
        {
            var response = await _client.PostAsJsonAsync("api/employee", emp);
            return await response.Content.ReadFromJsonAsync<Employee>();
        }

        // 更新
        public async Task UpdateAsync(Employee emp)
        {
            await _client.PutAsJsonAsync($"api/employee/{emp.Id}", emp);
        }

        // 削除
        public async Task DeleteAsync(int id)
        {
            await _client.DeleteAsync($"api/employee/{id}");
        }
    }
}

