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
            DisplayContent = true;
        }

        public Project tempProject;
        public Project TempProject
        {
            get
            {
                return tempProject;
            }
            set
            {
                tempProject = value;
                DisplayContent = false;
                NotifyPropertyChanged();
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
                NotifyPropertyChanged("Projects");
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

        public ObservableCollection<Project> Projects
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Project>(ProjectService.Current.Projects);
                }
                return new ObservableCollection<Project>(ProjectService.Current.SearchClients(Query));
            }
        }

        public ObservableCollection<Time> Times
        {
            get
            {   //get all times whos projectId = selectedproject.id
                if (selectedproject != null)
                {
                    return new ObservableCollection<Time>(TimeService.Current.Times.Where(s => s.ProjectId == selectedproject.Id));
                }

                return new ObservableCollection<Time>();
            }
        }

        public void Search()
        {
            NotifyPropertyChanged("Projects");
        }

        //Modify
        public void Add()
        {
            TempProject = new Project();
        }

        public void Edit()
        {
            TempProject = (Project)SelectedProject.Clone(); //Make a copy of selected cleint
            NotifyPropertyChanged("Projects");
        }

        public void Save()
        {
            Project projectInList = Projects.SingleOrDefault(s => s.Id == TempProject.Id);
            //if client doesn't exist add to list
            if (projectInList == null)
            {
                ProjectService.Current.Add(TempProject);
            }
            else
            {
                var idx = Projects.IndexOf(projectInList);
                ProjectService.Current.Projects[idx] = TempProject;
            }

            SelectedProject = TempProject;
            NotifyPropertyChanged("Projects");
        }

        public void Cancel()
        {
            //TempClient = null;
            DisplayContent = true;
        }

        public void Delete()
        {
            foreach (Time tm in Times)
            {
                TimeService.Current.Times.Remove(tm);
            }
            ProjectService.Current.Delete(SelectedProject);
            SelectedProject = null;
            NotifyPropertyChanged("Clients");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

