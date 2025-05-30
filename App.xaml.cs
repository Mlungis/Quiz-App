namespace The_Quiz
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginForm();
            MainPage = new NavigationPage(new LoginForm());
        }
    }
}
