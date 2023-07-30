using System;
using COP4870_New002.Library.Models;
using COP4870_New002.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using COP4870_New002.Library.DTO;
//using COP4870_New002.Library;

namespace COP4870_New002.MAUI.ViewModels
{
    public class ClientViewViewModel : INotifyPropertyChanged
    {
        public ClientViewViewModel()
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
                NotifyPropertyChanged(nameof(SelectedClient));
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
                NotifyPropertyChanged(nameof(MainClient));
                NotifyPropertyChanged(nameof(DisplayProjects));
                NotifyPropertyChanged(nameof(DisplayBills));
            }
        }

        public ClientDTO MainClient {
            get
            {
                var mainclient = new ClientDTO();
                if (SelectedClient != null)
                {
                    mainclient = ClientService.Current.Get(SelectedClient.Id);
                }
                return mainclient;
            }
        }

        private Project selectedproject;
        public Project SelectedProject
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

        private Bill selectedbill;
        public Bill SelectedBill
        {
            get
            {
                return selectedbill;
            }
            set
            {
                selectedbill = value;
                DisplayContent = true;
                NotifyPropertyChanged();
            }
        }

        private string query = string.Empty;
        public string Query
        {
            get
            {
                return query;
            }
            set
            {
                query = value;
                NotifyPropertyChanged(nameof(Clients));
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
                NotifyPropertyChanged(nameof(DisplayEdit));
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
                NotifyPropertyChanged(nameof(DisplayContent));
                NotifyPropertyChanged(nameof(DisplayProjects));
                NotifyPropertyChanged(nameof(DisplayBills));
            }
        }

        public bool DisplayProjects
        {
            get
            {
                NotifyPropertyChanged("Projects");
                return selectedmenucategory.Equals("Projects");
            }
        }
        public bool DisplayBills
        {
            get
            {
                NotifyPropertyChanged("Bills");
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

        public void Search()
        {
            NotifyPropertyChanged(nameof(Clients));
        }

        //Modify
        public void Add()
        {
            TempClient = new Client();
        }

        public void Edit()
        {
            TempClient = (Client)SelectedClient.Clone(); //Make a copy of selected cleint
            //NotifyPropertyChanged("Clients");
        }

        public void Save()
        {
            ClientService.Current.Add(TempClient);

            SelectedClient = TempClient;
            //TempClient = null;
            MenuCategory = string.Empty;
            NotifyPropertyChanged(nameof(Clients));
        }

        public void Cancel()
        {
            TempClient = null;
            DisplayContent = true;
        }

        public void Delete()
        {
            ClientService.Current.Delete(SelectedClient);
            SelectedClient = null;
            NotifyPropertyChanged(nameof(Clients));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

