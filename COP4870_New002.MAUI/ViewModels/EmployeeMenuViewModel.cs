using System;
using COP4870_New002.Library.Models;
using COP4870_New002.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace COP4870_New002.MAUI.ViewModels
{
	public class EmployeeMenuViewModel : INotifyPropertyChanged
	{
        public EmployeeMenuViewModel()
        {
            DisplayContent = true;
        }

        public Employee tempEmployee;
        public Employee TempEmployee
        {
            get
            {
                return tempEmployee;
            }
            set
            {
                tempEmployee = value;
                DisplayContent = false;
                NotifyPropertyChanged();
            }
        }


        private Employee selectedproject;
        public Employee SelectedEmployee
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
                NotifyPropertyChanged("Times");
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

        public ObservableCollection<Employee> Employees
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Employee>(EmployeeService.Current.Employees);
                }
                return new ObservableCollection<Employee>(EmployeeService.Current.SearchEmployees(Query));
            }
        }

        public ObservableCollection<Time> Times
        {
            get
            {   //get all times whos projectId = selectedproject.id
                if (selectedproject != null)
                {
                    var time_list = new ObservableCollection<Time>(TimeService.Current.Times.Where(s => s.EmployeeId == selectedproject.Id));
                    foreach (Time time_obj in time_list)
                    {
                        if (time_obj.Narrative.Length > 55)
                        {
                            time_obj.Narrative = time_obj.Narrative.Substring(0, 50);
                            time_obj.Narrative += "...";
                        }
                    }
                    return time_list;
                }

                return new ObservableCollection<Time>();
            }
        }

        public void Search()
        {
            NotifyPropertyChanged("Employees");
        }

        //Modify
        public void Add()
        {
            TempEmployee = new Employee();
        }

        public void Edit()
        {
            TempEmployee = (Employee)SelectedEmployee.Clone(); //Make a copy of selected cleint
            NotifyPropertyChanged("Employees");
        }

        public void Save()
        {
            Employee employeeInList = Employees.SingleOrDefault(s => s.Id == TempEmployee.Id);
            //if client doesn't exist add to list
            if (employeeInList == null)
            {
                EmployeeService.Current.Add(TempEmployee);
            }
            else
            {
                var idx = Employees.IndexOf(employeeInList);
                EmployeeService.Current.Employees[idx] = TempEmployee;
            }

            SelectedEmployee = TempEmployee;
            NotifyPropertyChanged("Employees");
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
            EmployeeService.Current.Delete(SelectedEmployee);
            SelectedEmployee = null;
            NotifyPropertyChanged("Employees");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

