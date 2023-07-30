using System;
using COP4870_New002.Library.Models;
using System.Windows.Input;
using COP4870_New002.Library.Services;
using System.Net.Sockets;

namespace COP4870_New002.MAUI.ViewModels
{
	public class ProjectViewModel
	{
        public Project Model { get; set; }
        public string ClientName
        {
            get
            {
                return ClientService.Current.GetName(Model.ClientId);
            }
        }

        private void ExecuteAdd()
        {
            //ProjectService.Current.Add(Model);
        }

        public ProjectViewModel()
        {
            Model = new Project();
        }

        public ProjectViewModel(int clientId)
        {

            Model = new Project { ClientId = clientId };
        }

        public ProjectViewModel(Project model)
        {
            Model = model;
        }
    }
}

