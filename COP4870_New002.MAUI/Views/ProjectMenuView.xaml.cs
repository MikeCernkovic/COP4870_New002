using COP4870_New002.MAUI.ViewModels;

namespace COP4870_New002.MAUI.Views;

public partial class ProjectMenuView : ContentPage
{
	public ProjectMenuView()
	{
		InitializeComponent();
        BindingContext = new ProjectViewViewModel();
    }

    void EditProject_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).EditProject();
    }

    void EditBill_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).EditBill();
    }


    void DeleteProject_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).DeleteProject();
    }

    void DeleteBill_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).DeleteBill();
    }

    void DeleteTime_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).RefreshTimes();
    }


    void AddProject_Clicked(System.Object sender, System.EventArgs e)
    {

        (BindingContext as ProjectViewViewModel).AddProject();
    }

    void AddBill_Clicked(System.Object sender, System.EventArgs e)
    {

        (BindingContext as ProjectViewViewModel).AddBill();
    }

    void Timer_Clicked(System.Object sender, System.EventArgs e)
    {

        (BindingContext as ProjectViewViewModel).Timer();
    }


    void SaveProject_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).SaveProject();
    }

    void SaveBill_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).SaveBill();
    }


    void Cancel_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).Cancel();
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
