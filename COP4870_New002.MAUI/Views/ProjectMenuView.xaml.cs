using COP4870_New002.MAUI.ViewModels;

namespace COP4870_New002.MAUI.Views;

public partial class ProjectMenuView : ContentPage
{
	public ProjectMenuView()
	{
		InitializeComponent();
        BindingContext = new ProjectMenuViewModel();
    }

    void EditProject_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).EditProject();
    }

    //void EditTime_Clicked(System.Object sender, System.EventArgs e)
    //{
    //    (BindingContext as ProjectMenuViewModel).EditTime();
    //}


    void DeleteProject_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).DeleteProject();
    }

    //void DeleteTime_Clicked(System.Object sender, System.EventArgs e)
    //{
    //    (BindingContext as ProjectMenuViewModel).DeleteTime();
    //}


    void AddProject_Clicked(System.Object sender, System.EventArgs e)
    {

        (BindingContext as ProjectMenuViewModel).AddProject();
    }

    //void AddTime_Clicked(System.Object sender, System.EventArgs e)
    //{

    //    (BindingContext as ProjectMenuViewModel).AddTime();
    //}


    void SaveProject_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).SaveProject();
    }

    //void SaveTime_Clicked(System.Object sender, System.EventArgs e)
    //{
    //    (BindingContext as ProjectMenuViewModel).SaveTime();
    //}


    void Cancel_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).Cancel();
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
