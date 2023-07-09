using COP4870_New002.MAUI.ViewModels;

namespace COP4870_New002.MAUI.Views;

public partial class EmployeeMenuView : ContentPage
{
    public EmployeeMenuView()
    {
        InitializeComponent();
        BindingContext = new EmployeeViewViewModel();
    }

    void Search_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as EmployeeViewViewModel).Search();
    }

    void Edit_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as EmployeeViewViewModel).Edit();
    }

    void Delete_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as EmployeeViewViewModel).Delete();
    }

    void Add_Clicked(System.Object sender, System.EventArgs e)
    {

        (BindingContext as EmployeeViewViewModel).Add();
    }

    void Save_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as EmployeeViewViewModel).Save();
    }

    void Cancel_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as EmployeeViewViewModel).Cancel();
    }

    void Toolbar_ClientsClicked(System.Object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//Client");
    }

    void Toolbar_ProjectsClicked(System.Object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//Project");
    }

    void Toolbar_EmployeesClicked(System.Object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//Employee");
    }
}
