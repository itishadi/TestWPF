// MainViewModel.cs
using MySqlConnector;
using System;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;

namespace TestWPF.Models
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DataTable _dataTable;
        private int _id;
        private string _firstName;
        private string _lastName;

        public DataTable DataTable
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                OnPropertyChanged(nameof(DataTable));
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadDataAsync()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=admintool;User=root;Password=;"))
                {
                    await connection.OpenAsync();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM entity", connection);

                    DataTable dt = new DataTable();
                    dt.Load(await cmd.ExecuteReaderAsync());

                    DataTable = dt;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"MySQL Error: {ex.Number} - {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task SaveDataAsync(int id, string firstName, string lastName)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=admintool;User=root;Password=;"))
                {
                    await connection.OpenAsync();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO entity (Id, FirstName, LastName) VALUES (@Id, @FirstName, @LastName)", connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);

                    await cmd.ExecuteNonQueryAsync();

                    // Refresh the data after inserting
                    await LoadDataAsync();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"MySQL Error: {ex.Number} - {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
