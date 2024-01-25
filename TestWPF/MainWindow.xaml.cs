using System;
using System.Windows;
using TestWPF.Models;

namespace TestWPF
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.LoadDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.LoadDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // MainWindow.xaml.cs
        private async void SaveData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Använd verkliga värden från TextBox-kontroller
                await _viewModel.SaveDataAsync(_viewModel.Id ,_viewModel.FirstName, _viewModel.LastName);

                // Uppdatera DataGrid
                await _viewModel.LoadDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }
}
