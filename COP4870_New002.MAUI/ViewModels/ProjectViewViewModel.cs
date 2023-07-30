using System;
using COP4870_New002.Library.Models;
using COP4870_New002.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using COP4870_New002.Library.DTO;

namespace COP4870_New002.MAUI.ViewModels
{
    public class ProjectViewViewModel : INotifyPropertyChanged
    {
        public ProjectViewViewModel()
        {
            DisplayProjectContent = true;
            DisplayBillContent = true;
        }

        public Project AddEditProject { get; set; }
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
                DisplayProjectContent = true;
                SelectedBill = null;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(MainProject));
                NotifyPropertyChanged(nameof(Times));
            }
        }

        public ProjectDTO MainProject
        {
            get
            {
                var mainproject = new ProjectDTO();
                if (SelectedProject != null)
                {
                    mainproject = ProjectService.Current.Get(SelectedProject.Id);
                }
                return mainproject;
            }
        }

        public Bill AddEditBill { get; set; }
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
                SelectedTime = null;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(MainBill));
                NotifyPropertyChanged(nameof(Times));
            }
        }

        public BillViewModel MainBill
        {
            get
            {
                var mainbill = new BillDTO();
                if (SelectedBill != null)
                {
                    mainbill = BillService.Current.Get(SelectedBill.Id);
                }
                return new BillViewModel(mainbill);
            }
        }

        //public TimeViewModel tempTime;
        private TimeViewModel selectedtime;
        public TimeViewModel SelectedTime
        {
            get
            {
                return selectedtime;
            }
            set
            {
                selectedtime = value;
                DisplayBillContent = true;
                NotifyPropertyChanged();
            }
        }

        public Client SelectedClient { get; set; }

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
                NotifyPropertyChanged(nameof(Projects));
            }
        }

        private bool displayprojectcontent;
        public bool DisplayProjectContent
        {
            get
            {
                return displayprojectcontent;
            }
            set
            {
                displayprojectcontent = value;
                //DisplayTimeContent = true;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(DisplayProjectEdit));
            }
        }

        public bool DisplayProjectEdit
        {
            get
            {
                return !(DisplayProjectContent);
            }
        }

        private bool displaybillcontent;
        public bool DisplayBillContent
        {
            get
            {
                return displaybillcontent;
            }

            set
            {
                displaybillcontent = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(DisplayBillEdit));
            }
        }

        public bool DisplayBillEdit
        {
            get
            {
                return !(displaybillcontent);
            }
        }

        public ObservableCollection<Client> Clients
        {
            get
            {
                return new ObservableCollection<Client>(ClientService.Current.Clients);
            }
        }

        public ObservableCollection<Project> Projects
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Project>(ProjectService.Current.Projects);
                }
                return new ObservableCollection<Project>(ProjectService.Current.SearchProjects(Query));
            }
        }

        public ObservableCollection<TimeViewModel> Times
        {
            get
            {
                if (selectedbill != null)
                {
                    return new ObservableCollection<TimeViewModel>(
                        MainBill.Model.Times.Select(t => new TimeViewModel(t)));
                }
                return new ObservableCollection<TimeViewModel>();
            }
        }

        //ADD
        public void AddBill()
        {
            if (SelectedProject != null)
            {
                var newbill = new Bill()
                {
                    TotalAmount = 0,
                    ProjectId = SelectedProject.Id,
                    ClientId = SelectedProject.ClientId,
                    IsActive = true
                };
                BillService.Current.Add(newbill);
                NotifyPropertyChanged(nameof(MainProject));
            }
        }

        public void AddProject()
        {
            AddEditProject = new Project();
            DisplayProjectContent = false;
            NotifyPropertyChanged(nameof(AddEditProject));
        }

        public void Timer()
        {
            if (selectedbill != null)
            {
                MainBill.ExecuteTimer();
            }
        }

        //EDIT
        public void EditProject()
        {
            if(SelectedProject != null)
            {
                AddEditProject = SelectedProject;
                SelectedClient = ClientService.Current.Clients
                    .FirstOrDefault(c => c.Id == AddEditProject.ClientId);
         
                DisplayProjectContent = false;
                NotifyPropertyChanged(nameof(AddEditProject));
            }
        }

        public void EditBill()
        {
            if (SelectedBill != null)
            {
                AddEditBill = SelectedBill;
                DisplayBillContent = false;
                NotifyPropertyChanged(nameof(AddEditBill));
            }
        }

        //SAVE
        public void SaveProject()
        {
            if (SelectedClient != null)
            {
                AddEditProject.ClientId = SelectedClient.Id;
                ProjectService.Current.Add(AddEditProject);

                SelectedProject = AddEditProject;
                AddEditProject = null;
                SelectedClient = null;
                DisplayProjectContent = true;
                NotifyPropertyChanged(nameof(Projects));
                NotifyPropertyChanged(nameof(MainProject));
            }
        }

        public void SaveBill()
        {
            BillService.Current.Add(AddEditBill);

            AddEditBill = null;
            DisplayBillContent = true;
            NotifyPropertyChanged(nameof(MainProject));
        }

        //DELETE
        public void DeleteProject()
        {
            if (selectedproject != null)
            {
                ProjectService.Current.Delete(MainProject.project);
                SelectedProject = null;
                NotifyPropertyChanged(nameof(Projects));
            }
        }

        public void DeleteBill()
        {
            if (selectedbill != null)
            {
                BillService.Current.Delete(MainBill.Model.bill);
                SelectedBill = null;
                NotifyPropertyChanged(nameof(MainProject));
            }
        }

        public void RefreshTimes()
        {
            SelectedTime = null;
            NotifyPropertyChanged(nameof(Times));
        }

        public void Cancel()
        {
            AddEditProject = null;
            AddEditBill = null;

            DisplayProjectContent = true;
            DisplayBillContent = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

