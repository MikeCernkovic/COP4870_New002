using System;
using COP4870_New002.Library.Models;
using COP4870_New002.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace COP4870_New002.MAUI.ViewModels
{
    public class ProjectMenuViewModel : INotifyPropertyChanged
    {
        public ProjectMenuViewModel()
        {
            DisplayProjectContent = true;
            DisplayTimeContent = true;
        }

        public Project tempProject;
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
                //SelectedTime = null;
                NotifyPropertyChanged();
                NotifyPropertyChanged("SelectedClient");
                NotifyPropertyChanged("Times");
            }
        }

        public Time tempTime;
        private Time selectedtime;
        public Time SelectedTime
        {
            get
            {
                return selectedtime;
            }
            set
            {
                selectedtime = value;
                if(selectedtime != null)
                {
                    SelectedEmployee = EmployeeService.Current.Get(selectedtime.EmployeeId);
                }
                else
                {
                    SelectedEmployee = null;
                }
                
                DisplayTimeContent = true;
                NotifyPropertyChanged();
            }
        }

        public Employee SelectedEmployee { get; set; }

        private Client selectedclient;
        public Client SelectedClient
        {
            get
            {
                if(SelectedProject == null)
                {
                    return null;
                }
                return BillService.Current.Get(SelectedProject.ClientId);
            }
            set
            {
                selectedclient = value;
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

        private bool displaytimecontent;
        public bool DisplayTimeContent
        {
            get
            {
                return displaytimecontent;
            }

            set
            {
                displaytimecontent = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("DisplayTimeEdit");
            }
        }
        public bool DisplayTimeEdit
        {
            get
            {
                return !(displaytimecontent);
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
                return new ObservableCollection<Project>(ProjectService.Current.SearchProjects(query));
            }
        }

        public ObservableCollection<Time> Times
        {
            get
            {
                if (selectedproject != null)
                {
                    var time_list = new ObservableCollection<Time>(TimeService.Current.Times.Where(t => t.ProjectId == selectedproject.Id));
                    return time_list;
                }
                return new ObservableCollection<Time>();
            }
        }

        public ObservableCollection<Employee> Employees
        {
            get
            {
                return new ObservableCollection<Employee>(EmployeeService.Current.Employees);
            }
        }

        public ObservableCollection<Client> Clients
        {
            get
            {
                return new ObservableCollection<Client>(BillService.Current.Clients);
            }
        }

        public void AddTime()
        {
            tempTime = SelectedTime;
            SelectedTime = new Time();
            DisplayTimeContent = false;
        }

        public void EditTime()
        {
            tempTime = SelectedTime;
            SelectedTime = (Time)tempTime.Clone();
            DisplayTimeContent = false;
        }

        public void SaveTime()
        {
            SelectedTime.EmployeeId = SelectedEmployee.Id;
            SelectedTime.ProjectId = SelectedProject.Id;

            Time timeInList = TimeService.Current.Get(SelectedTime.Id);
            if(timeInList == null)
            {
                TimeService.Current.Add(SelectedTime);
            }
            else
            {
                var idx = TimeService.Current.Times.IndexOf(tempTime);
                TimeService.Current.Times[idx] = SelectedTime;
            }

            DisplayTimeContent = true;
            NotifyPropertyChanged("Times");
            NotifyPropertyChanged("SelectedTime");
        }

        public void DeleteTime()
        {
            TimeService.Current.Delete(SelectedTime);
            SelectedTime = null;
            NotifyPropertyChanged("Times");
        }



        public void AddProject()
        {
            tempProject = SelectedProject;
            SelectedProject = new Project();
            DisplayProjectContent = false;
        }

        public void EditProject()
        {
            tempProject = SelectedProject;
            SelectedProject = (Project)tempProject.Clone();
            DisplayProjectContent = false;
        }

        public void SaveProject()
        {
            //Set client Id
            SelectedProject.ClientId = selectedclient.Id;

            Project projectInList = ProjectService.Current.Get(SelectedProject.Id);
            if(projectInList == null)
            {
                ProjectService.Current.Add(SelectedProject);
            }
            else
            {
                var idx = ProjectService.Current.Projects.IndexOf(tempProject);
                ProjectService.Current.Projects[idx] = SelectedProject;
            }

            SelectedTime = null;
            DisplayTimeContent = true;
            DisplayProjectContent = true;
            NotifyPropertyChanged("Projects");
            NotifyPropertyChanged("SelectedClient");
            NotifyPropertyChanged("SelectedProject");
        }

        public void DeleteProject()
        {
            foreach(Time time in Times)
            {
                TimeService.Current.Delete(time.Id);
            }
            ProjectService.Current.Delete(SelectedProject);
            SelectedTime = null;
            SelectedProject = null;
            NotifyPropertyChanged("Projects");
        }


        public void Cancel()
        {
            SelectedProject = tempProject;
            SelectedTime = tempTime;

            DisplayProjectContent = true;
            DisplayTimeContent = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

