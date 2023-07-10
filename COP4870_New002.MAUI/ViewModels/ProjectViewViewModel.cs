using System;
using COP4870_New002.Library.Models;
using COP4870_New002.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace COP4870_New002.MAUI.ViewModels
{
    public class ProjectViewViewModel : INotifyPropertyChanged
    {
        public ProjectViewViewModel()
        {
            DisplayProjectContent = true;
            //DisplayTimeContent = true;
        }

        public Project AddEditProject { get; set; }
        private ProjectViewModel selectedproject;
        public ProjectViewModel SelectedProject
        {
            get
            {
                return selectedproject;
            }
            set
            {
                selectedproject = value;
                SelectedBill = null;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Bills));
                NotifyPropertyChanged(nameof(Times));
            }
        }

        private BillViewModel selectedbill;
        public BillViewModel SelectedBill
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
                NotifyPropertyChanged(nameof(Times));
            }
        }

        public TimeViewModel tempTime;
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
                NotifyPropertyChanged("Projects");
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

        //private bool displaytimecontent;
        //public bool DisplayTimeContent
        //{
        //    get
        //    {
        //        return displaytimecontent;
        //    }

        //    set
        //    {
        //        displaytimecontent = value;
        //        NotifyPropertyChanged();
        //        NotifyPropertyChanged("DisplayTimeEdit");
        //    }
        //}
        //public bool DisplayTimeEdit
        //{
        //    get
        //    {
        //        return !(displaytimecontent);
        //    }
        //}

        public ObservableCollection<Client> Clients
        {
            get
            {
                return new ObservableCollection<Client>(ClientService.Current.Clients);
            }
        }

        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<ProjectViewModel>(ProjectService.Current.Projects.Select(p => new ProjectViewModel(p)));
                }
                return new ObservableCollection<ProjectViewModel>(ProjectService.Current.SearchProjects(query).Select(p => new ProjectViewModel(p)));
            }
        }

        public ObservableCollection<BillViewModel> Bills
        {
            get
            {
                if (selectedproject != null)
                {
                    return new ObservableCollection<BillViewModel>(BillService.Current.Bills.Where(b => b.ProjectId == selectedproject.Model.Id).Select(m => new BillViewModel(m)));
                }
                return new ObservableCollection<BillViewModel>();
            }
        }

        public ObservableCollection<TimeViewModel> Times
        {
            get
            {
                if (selectedbill != null)
                {
                    return new ObservableCollection<TimeViewModel>(TimeService.Current.Times.Where(t => t.BillId == selectedbill.Model.Id).Select(m => new TimeViewModel(m)));
                }
                return new ObservableCollection<TimeViewModel>();
            }
        }

        //ADD
        public void AddBill()
        {
            if(SelectedProject != null)
            {
                var newbill = new Bill()
                {
                    TotalAmount = 0,
                    ProjectId = SelectedProject.Model.Id,
                    ClientId = SelectedProject.Model.ClientId,
                    IsActive = true
                };
                BillService.Current.Add(newbill);
                NotifyPropertyChanged(nameof(Bills));
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
            if(selectedbill != null)
            {
                SelectedBill.ExecuteTimer();
            }
        }

        //EDIT
        public void EditProject()
        {
            AddEditProject = SelectedProject.Model;
            SelectedClient = ClientService.Current.Get(AddEditProject.ClientId);
            DisplayProjectContent = false;
            NotifyPropertyChanged(nameof(AddEditProject));
        }

        //SAVE
        public void SaveProject()
        {
            if(SelectedClient != null)
            {
                AddEditProject.ClientId = SelectedClient.Id;
                if (ProjectService.Current.Get(AddEditProject.Id) == null)
                {                
                    ProjectService.Current.Add(AddEditProject);  
                }
                else
                {
                    int idx = ProjectService.Current.Projects.IndexOf(AddEditProject);
                    ProjectService.Current.Projects[idx] = AddEditProject;
                }

                SelectedProject = new ProjectViewModel(AddEditProject);
                AddEditProject = null;
                SelectedClient = null;
                DisplayProjectContent = true;
                NotifyPropertyChanged(nameof(Projects));
            }
        }

        //DELETE
        public void DeleteProject()
        {
            if(selectedproject != null)
            {
                ProjectService.Current.Delete(SelectedProject.Model.Id);
                SelectedProject = null;
                NotifyPropertyChanged(nameof(Projects));
            }
        }

        public void DeleteBill()
        {
            if(selectedbill != null)
            {
                BillService.Current.Delete(SelectedBill.Model.Id);
                SelectedBill = null;
                NotifyPropertyChanged(nameof(Bills));
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
            //SelectedTime = tempTime;

            DisplayProjectContent = true;
            //DisplayTimeContent = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

