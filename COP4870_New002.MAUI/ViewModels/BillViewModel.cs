using COP4870_New002.Library.Models;
using COP4870_New002.Library.Services;
using COP4870_New002.MAUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace COP4870_New002.MAUI.ViewModels
{
	public class BillViewModel
	{
        public Bill Model { get; set; }

        //public ICommand AddCommand { get; private set; }
        //public ICommand TimerCommand { get; private set; }

        private void ExecuteAdd()
        {
            BillService.Current.Add(Model);
            //Shell.Current.GoToAsync($"//ClientDetail?clientId={Model.ClientId}");
        }

        public void ExecuteTimer()
        {
            var window = new Window();
            window.MinimumWidth = 250;
            window.MaximumWidth = 250;
            window.MinimumHeight = 350;
            window.MaximumHeight = 350;

            //Dispatcher.Dispatch(() =>
            //{
            //    window.MinimumWidth = 0;
            //    window.MinimumHeight = 0;
            //    window.MaximumWidth = double.PositiveInfinity;
            //    window.MaximumHeight = double.PositiveInfinity;
            //});

            var view = new TimerView(Model.Id, window);
            window.Page = view;
            Application.Current.OpenWindow(window);
        }

        public void SetupCommands()
        {
            //AddCommand = new Command(ExecuteAdd);
            //TimerCommand = new Command(ExecuteTimer);
        }

        public BillViewModel()
        {
            Model = new Bill();
            SetupCommands();
        }

        //public BillViewModel(int clientId)
        //{

        //    //Model = new Bill { ClientId = clientId };
        //    SetupCommands();
        //}

        public BillViewModel(Bill model)
        {
            Model = model;
            SetupCommands();
        }
    }
}

