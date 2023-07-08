using System;
using COP4870_New002.Library.Models;
using COP4870_New002.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//using COP4870_New002.Library;

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

        private string query;
        public string Query
        {
            get
            {
                return query;
            }
            set
            {
                query = value;
                NotifyPropertyChanged("Clients");
            }
        }

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

        //IsVisible="{Binding Path=DisplayBills}" IsEnabled="{Binding Path=DisplayBills}"
        private string selectedmenucategory = string.Empty;
        public string MenuCategory
        {
            set
            {
                selectedmenucategory = value;
                NotifyPropertyChanged(nameof(DisplayProjects));
                NotifyPropertyChanged(nameof(DisplayBills));
            }
        }

        public bool DisplayProjects
        {
            get
            {
                return selectedmenucategory.Equals("Projects");
            }
        }
        public bool DisplayBills
        {
            get
            {
                return selectedmenucategory.Equals("Bills");
            }
        }



        public ObservableCollection<Client> Clients
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Client>(ClientService.Current.Clients);
                }
                return new ObservableCollection<Client>(ClientService.Current.SearchClients(Query));
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

        public ObservableCollection<Bill> Bills
        {
            get
            {   //get all projects whos clientId = selectedclient.id
                if (selectedclient != null)
                {
                    return new ObservableCollection<Bill>(BillService.Current.Bills.Where(b => b.ClientId == selectedclient.Id));
                }

                return new ObservableCollection<Bill>();
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
            Client clientInList = ClientService.Current.Get(TempClient.Id);

            //if client doesn't exist add to list
            if (clientInList == null)
            {
                ClientService.Current.Add(TempClient);
            }
            else
            {
                var idx = Clients.IndexOf(clientInList);
                ClientService.Current.Clients[idx] = TempClient;
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
            ClientService.Current.Delete(SelectedClient);
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

