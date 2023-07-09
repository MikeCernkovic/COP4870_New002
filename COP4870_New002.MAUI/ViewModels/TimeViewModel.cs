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

        private void ExecuteAdd()
        {
            TimeService.Current.Add(Model);
            //Shell.Current.GoToAsync($"//ClientDetail?clientId={Model.ClientId}");
        }

        public TimeViewModel()
        {
            Model = new Time();
        }

        //public BillViewModel(int clientId)
        //{

        //    //Model = new Bill { ClientId = clientId };
        //    SetupCommands();
        //}

        public TimeViewModel(Time model)
        {
            Model = model;
        }
    }
}

