using System;
using COP4870_New002.Library.Models;
using COP4870_New002.Library.Services;
using System.Windows.Input;

namespace COP4870_New002.MAUI.ViewModels
{
	public class TimeViewModel
	{
        public Time Model { get; set; }
        public string EmployeeName
        {
            get
            {
                var em = EmployeeService.Current.Get(Model.EmployeeId);
                if(em == null)
                {
                    return string.Empty;
                }
                return em.Name;
            }
        }

        public ICommand DeleteCommand { get; private set; }

        private void ExecuteDelete()
        {
            TimeService.Current.Delete(Model);
            //Shell.Current.GoToAsync($"//ClientDetail?clientId={Model.ClientId}");
        }

        private void SetupCommands()
        {
            DeleteCommand = new Command(ExecuteDelete);
        }

        public TimeViewModel()
        {
            Model = new Time();
            SetupCommands();
        }

        public TimeViewModel(Time model)
        {
            Model = model;
            SetupCommands();
        }
    }
}

