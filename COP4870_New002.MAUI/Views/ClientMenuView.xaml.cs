using COP4870_New002.MAUI.ViewModels;

namespace COP4870_New002.MAUI.Views;

public partial class ClientMenuView : ContentPage
{
	public ClientMenuView()
	{
		InitializeComponent();
		BindingContext = new ClientMenuViewModel();
	}

    void Search_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ClientMenuViewModel).Search();
    }

    void Edit_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ClientMenuViewModel).Edit();
    }

    void Delete_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ClientMenuViewModel).Delete();
    }

    void Add_Clicked(System.Object sender, System.EventArgs e)
    {

        (BindingContext as ClientMenuViewModel).Add();
    }

    void Save_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ClientMenuViewModel).Save();
    }

    void Cancel_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ClientMenuViewModel).Cancel();
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
