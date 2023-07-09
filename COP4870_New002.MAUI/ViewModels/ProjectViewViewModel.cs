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

        public ProjectViewModel tempProject;
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
                NotifyPropertyChanged("DisplayProjectEdit");
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

        //public void EditTime()
        //{
        //    tempTime = SelectedTime;
        //    SelectedTime = (Time)tempTime.Clone();
        //    DisplayTimeContent = false;
        //}

        //public void SaveTime()
        //{
        //    SelectedTime.EmployeeId = SelectedEmployee.Id;
        //    SelectedTime.ProjectId = SelectedProject.Id;

        //    Time timeInList = TimeService.Current.Get(SelectedTime.Id);
        //    if(timeInList == null)
        //    {
        //        TimeService.Current.Add(SelectedTime);
        //    }
        //    else
        //    {
        //        var idx = TimeService.Current.Times.IndexOf(tempTime);
        //        TimeService.Current.Times[idx] = SelectedTime;
        //    }

        //    DisplayTimeContent = true;
        //    NotifyPropertyChanged("Times");
        //    NotifyPropertyChanged("SelectedTime");
        //}

        //public void DeleteTime()
        //{
        //    TimeService.Current.Delete(SelectedTime);
        //    SelectedTime = null;
        //    NotifyPropertyChanged("Times");
        //}

        //public void AddProject()
        //{
        //    //tempProject = SelectedProject;
        //    //SelectedProject = new Project();
        //    //DisplayProjectContent = false;
        //}

        public void Timer()
        {
            if(selectedbill != null)
            {
                SelectedBill.ExecuteTimer();
            }
        }


        //public void EditProject()
        //{
        //    //tempProject = SelectedProject;
        //    //SelectedProject = (Project)tempProject.Clone();
        //    //DisplayProjectContent = false;
        //}

        //public void SaveProject()
        //{
        //    //Set client Id
        //    SelectedProject.ClientId = selectedclient.Id;

        //    Project projectInList = ProjectService.Current.Get(SelectedProject.Id);
        //    if(projectInList == null)
        //    {
        //        ProjectService.Current.Add(SelectedProject);
        //    }
        //    else
        //    {
        //        var idx = ProjectService.Current.Projects.IndexOf(tempProject);
        //        ProjectService.Current.Projects[idx] = SelectedProject;
        //    }

        //    //SelectedTime = null;
        //    //DisplayTimeContent = true;
        //    DisplayProjectContent = true;
        //    NotifyPropertyChanged("Projects");
        //    NotifyPropertyChanged("SelectedClient");
        //    NotifyPropertyChanged("SelectedProject");
        //}

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
            SelectedProject = tempProject;
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

