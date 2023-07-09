using COP4870_New002.MAUI.ViewModels;

namespace COP4870_New002.MAUI.Views;

public partial class TimerView : ContentPage
{
    public TimerView(int projectId, Window parentWindow)
    {
        InitializeComponent();
        BindingContext = new TimerViewModel(projectId, parentWindow);
    }
}
