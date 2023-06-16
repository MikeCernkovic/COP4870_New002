using COP4870_New002.MAUI.ViewModels;

namespace COP4870_New002.MAUI.Views;

public partial class ProjectMenuView : ContentPage
{
	public ProjectMenuView()
	{
		InitializeComponent();
        BindingContext = new ProjectMenuViewModel();
    }

    void Search_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).Search();
    }

    void Edit_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).Edit();
    }

    void Delete_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).Delete();
    }

    void Add_Clicked(System.Object sender, System.EventArgs e)
    {

        (BindingContext as ProjectMenuViewModel).Add();
    }

    void Save_Clicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).Save();
    }

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
}
