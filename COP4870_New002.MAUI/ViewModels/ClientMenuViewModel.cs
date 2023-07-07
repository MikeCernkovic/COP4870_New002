using System;
using COP4870_New002.Library.Models;
using COP4870_New002.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace COP4870_New002.MAUI.ViewModels
{
    public class ClientMenuViewModel : INotifyPropertyChanged
    {
        public ClientMenuViewModel()
        {
            DisplayContent = true;
        }

        public Client tempClient;
        public Client TempClient
        {
            get
            {
                return tempClient;
            }
            set
            {
                tempClient = value;
                DisplayContent = false;
                NotifyPropertyChanged();
            }
        }


        private Client selectedclient;
        public Client SelectedClient
        {
            get
            {
                return selectedclient;
            }
            set
            {
                selectedclient = value;
                DisplayContent = true;
                NotifyPropertyChanged();
                NotifyPropertyChanged("Projects");
            }
        }

        private Client selectedproject;
        public Client SelectedProject
        {
            get
            {
                return selectedproject;
            }
            set
            {
                selectedproject = value;
                DisplayContent = true;
                NotifyPropertyChanged();
            }
        }

        public string Query { get; set; }

        private bool display = true;
        public bool DisplayContent
        {
            get
            {
                return display;
            }

            set
            {
                display = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("DisplayEdit");
            }
        }
        public bool DisplayEdit
        {
            get
            {
                return !(DisplayContent);
            }
        }

        public ObservableCollection<Client> Clients
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Client>(BillService.Current.Clients);
                }
                return new ObservableCollection<Client>(BillService.Current.SearchClients(Query));
            }
        }

        public ObservableCollection<Project> Projects
        {
            get
            {   //get all projects whos clientId = selectedclient.id
                if (selectedclient != null)
                {
                    return new ObservableCollection<Project>(ProjectService.Current.Projects.Where(s => s.ClientId == selectedclient.Id));
                }

                return new ObservableCollection<Project>();
            }
        }

        public void Search()
        {
            NotifyPropertyChanged("Clients");
        }

        //Modify
        public void Add()
        {
            TempClient = new Client();
        }

        public void Edit()
        {
            TempClient = (Client)SelectedClient.Clone(); //Make a copy of selected cleint
            NotifyPropertyChanged("Clients");
        }

        public void Save()
        {
            //Client clientInList = Clients.SingleOrDefault(s => s.Id == TempClient.Id);
            Client clientInList = BillService.Current.Get(TempClient.Id);

            //if client doesn't exist add to list
            if (clientInList == null)
            {
                BillService.Current.Add(TempClient);
            }
            else
            {
                var idx = Clients.IndexOf(clientInList);
                BillService.Current.Clients[idx] = TempClient;
            }

            SelectedClient = TempClient;
            NotifyPropertyChanged("Clients");
        }

        public void Cancel()
        {
            //TempClient = null;
            DisplayContent = true;
        }

        public void Delete()
        {
            foreach (Project proj in Projects)
            {
                proj.ClientId = 0;
            }
            BillService.Current.Delete(SelectedClient);
            SelectedClient = null;
            NotifyPropertyChanged("Clients");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

