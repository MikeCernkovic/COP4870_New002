using COP4870_New002.Library.Models;
using COP4870_New002.MAUI.ViewModels;

namespace COP4870_New002.MAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        void ClientsClicked(System.Object sender, System.EventArgs e)
        {
            Shell.Current.GoToAsync("//Client");
        }

        void ProjectsClicked(System.Object sender, System.EventArgs e)
        {
            Shell.Current.GoToAsync("//Project");
        }
    }
}




