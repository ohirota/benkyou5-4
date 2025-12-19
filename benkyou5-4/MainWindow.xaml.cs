using SharedModels;    // Employee.cs の場所に合わせて変更
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using benkyou5_4.Services;



namespace benkyou5_4
{
    public partial class MainWindow : Window
    {
        private readonly EmployeeApiService _service = new EmployeeApiService(); //エラー　ｃｓ０２４６

        private readonly HttpClient _client = new HttpClient();


        public MainWindow()
        {
            InitializeComponent();

            // Web API のURLに合わせる (ポート番号は自分の環境に合わせて変更)
            _client.BaseAddress = new Uri("https://localhost:7122/");

            // 起動したら社員一覧を読み込む
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            // Web API から社員一覧を取得
var employees = await _client.GetFromJsonAsync<List<Employee>>("api/employee");

            // DataGrid に表示
            EmployeeGrid.ItemsSource = employees;
        }

        private async void ReloadButton_Click(object sender, RoutedEventArgs e)　
        {
            EmployeeGrid.ItemsSource = await _service.GetAllAsync();
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var emp = new Employee
            {
                Name = NameBox.Text,　　//エラー　cs0103
                Department = DeptBox.Text,//エラー　cs0103
                Age = int.Parse(AgeBox.Text)　　　//エラー　cs0103
            };
            await _service.AddAsync(emp);
            EmployeeGrid.ItemsSource = await _service.GetAllAsync();
        }


    }
}

