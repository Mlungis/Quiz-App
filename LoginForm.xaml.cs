using Microsoft.Maui.Controls;
using Microsoft.Data.Sqlite;
using The_Quiz.Users;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace The_Quiz
{
    public partial class LoginForm : ContentPage
    {
        public ICommand SignUpCommand { get; }

        private readonly string dbPath;

        public LoginForm()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            SignUpCommand = new Command(OnSignUp);
            BindingContext = this;

            var userDb = new UserDatabase();
            dbPath = userDb.GetDbPath();
        }

        private async void OnSignUp()
        {
            await Navigation.PushAsync(new SignUp());
        }

        private async void OnSignInClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Validation Error", "Please enter both email and password.", "OK");
                return;
            }

            bool isValidUser = await ValidateUserAsync(email, password);

            if (isValidUser)
            {
                await DisplayAlert("Success", "Login successful!", "OK");
                await Navigation.PushAsync(new Dashboard());
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid email or password.", "Try Again");
            }
        }

        private async Task<bool> ValidateUserAsync(string email, string password)
        {
            try
            {
                using var connection = new SqliteConnection($"Data Source={dbPath}");
                await connection.OpenAsync();

                var query = "SELECT COUNT(1) FROM Users WHERE Email = $email AND Password = $password";
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("$email", email);
                command.Parameters.AddWithValue("$password", password);

                var result = await command.ExecuteScalarAsync();
                return result != null && Convert.ToInt32(result) == 1;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                return false;
            }
        }

        private async void OnSignUpTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }
    }
}
