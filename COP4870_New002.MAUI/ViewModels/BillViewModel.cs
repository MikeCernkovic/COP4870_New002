using System;
using COP4870_New002.Library.Models;
using COP4870_New002.Library.Services;
using System.Windows.Input;
using COP4870_New002.MAUI.Views;

namespace COP4870_New002.MAUI.ViewModels
{
	public class BillViewModel
	{
        public Bill Model { get; set; }

        //public ICommand AddCommand { get; private set; }
        public ICommand TimerCommand { get; private set; }

        private void ExecuteAdd()
        {
            BillService.Current.Add(Model);
            //Shell.Current.GoToAsync($"//ClientDetail?clientId={Model.ClientId}");
        }

        private void ExecuteTimer()
        {

            //var window = new Window()
            //{
            //    Width = 250,
            //    Height = 350,
            //    X = 0,
            //    Y = 0
            //};
            //var view = new TimerView(Model.Id, window);
            //window.Page = view;
            //Application.Current.OpenWindow(window);
        }

        public void SetupCommands()
        {
            //AddCommand = new Command(ExecuteAdd);
            TimerCommand = new Command(ExecuteTimer);
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

