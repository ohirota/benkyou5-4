using SharedModels;
using System;
using System.Collections.Generic;
using System.Windows;
using benkyou5_4.Services; // ← これを追加！
using SharedModels;

namespace benkyou5_4
{
    public partial class MainWindow : Window
    {
        private readonly EmployeeApiService _service = new EmployeeApiService();

        public MainWindow()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            try
            {
                var employees = await _service.GetAllAsync();
                EmployeeGrid.ItemsSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"読み込みエラー: {ex.Message}");
            }
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e) => LoadEmployees();

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var emp = new Employee
                {
                    Name = NameBox.Text,
                    Department = DeptBox.Text,
                    HireDate = HireDatePicker.SelectedDate ?? DateTime.Now
                };
                await _service.AddAsync(emp);
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"追加エラー: {ex.Message}");
            }
        }

        // --- ここが GitHub 上で不足している部分です ---

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Employee)EmployeeGrid.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("更新する行を選んでください");
                return;
            }

            selectedItem.Name = NameBox.Text;
            selectedItem.Department = DeptBox.Text;
            selectedItem.HireDate = HireDatePicker.SelectedDate ?? DateTime.Now;

            await _service.UpdateAsync(selectedItem);
            LoadEmployees();
            MessageBox.Show("更新しました");
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Employee)EmployeeGrid.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("削除する行を選んでください");
                return;
            }

            if (MessageBox.Show("本当に削除しますか？", "確認", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await _service.DeleteAsync(selectedItem.Id);
                LoadEmployees();
            }
        }
    }
}

