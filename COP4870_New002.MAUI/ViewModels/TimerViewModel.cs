using System;
using COP4870_New002.Library.Models;
using COP4870_New002.Library.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace COP4870_New002.MAUI.ViewModels
{
	public class TimerViewModel : INotifyPropertyChanged
	{
        private Bill bill { get; set; }

        public string ProjectName
        {
            get
            {
                return ProjectService.Current.Get(bill.ProjectId).Name;
            }
        }
        public List<Employee> Employees
        {
            get
            {
                return EmployeeService.Current.Employees;
            }
        }

        public Employee SelectedEmployee { get; set; }
        public string Narrative { get; set; }

        public string TimerDisplay
        {
            get
            {
                var time = stopwatch.Elapsed;
                var str = string.Format("{0:00}:{1:00}:{2:00}",
                time.Hours,
                time.Minutes,
                time.Seconds);
                return str;
            }
        }

        private Window parentWindow;

        private IDispatcherTimer timer { get; set; }
        private Stopwatch stopwatch { get; set; }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand SubmitCommand { get; private set; }

        public void ExecuteStart()
        {
            stopwatch.Start();
            timer.Start();
        }

        public void ExecuteStop()
        {
            stopwatch.Stop();
        }

        public void ExecuteSubmit()
        {
            if(SelectedEmployee != null)
            {
                Time newtime = new Time()
                {
                    Date = DateTime.Now,
                    Narrative = Narrative,
                    Hours = stopwatch.Elapsed.TotalHours,
                    BillId = bill.Id,
                    EmployeeId = SelectedEmployee.Id
                };
                TimeService.Current.Add(newtime);
                bill = null;
                SelectedEmployee = null;
            }
            
            Application.Current.CloseWindow(parentWindow);
        }

        private void SetupCommands()
        {
            StartCommand = new Command(ExecuteStart);
            StopCommand = new Command(ExecuteStop);
            SubmitCommand = new Command(ExecuteSubmit);
        }

        public TimerViewModel(int billId, Window parentWindow)
        {
            bill = BillService.Current.Get(billId);
            stopwatch = new Stopwatch();
            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.IsRepeating = true;

            timer.Tick += Timer_Tick;
            SetupCommands();
            this.parentWindow = parentWindow;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timer.IsRunning)
            {
                NotifyPropertyChanged(nameof(TimerDisplay));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

