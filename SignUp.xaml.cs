using Microsoft.Maui.Controls;
using The_Quiz.Users;
using System;

namespace The_Quiz
{
    public partial class SignUp : ContentPage
    {
        private readonly UserDatabase userDb;

        public SignUp()
        {
            InitializeComponent();
            userDb = new UserDatabase();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;
            string username = UsernameEntry.Text;

            if (string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                await DisplayAlert("Validation Error", "All fields are required.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Validation Error", "Passwords do not match.", "OK");
                return;
            }


            if (!(email.Contains("@") && email.Contains(".com")))
            {
                await DisplayAlert("Validation Error", "Please enter a valid email address.", "OK");
                return;
            }

            try
            {
                await userDb.AddUserAsync(username, email, password);
                await DisplayAlert("Success", "Account created successfully!", "OK");
                await Navigation.PushAsync(new LoginForm());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to save data: {ex.Message}", "OK");
            }
        }
    }
}
