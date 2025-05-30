using Microsoft.Maui.Controls;

namespace The_Quiz
{
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private async void OnPlayClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Quiz());
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
#if ANDROID
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#endif
        }
    }
}
