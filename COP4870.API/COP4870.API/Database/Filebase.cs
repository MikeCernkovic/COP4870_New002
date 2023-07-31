using System;
using Newtonsoft.Json;
using COP4870_New002.Library.Models;

namespace COP4870.API.Database
{
	public class Filebase
	{
        private string _root;
        private string _clientRoot;
        private string _projectRoot;
        private string _employeeRoot;
        private string _billRoot;
        private string _timeRoot;
        private static Filebase? _instance;

        public static Filebase Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = @"/Users/michaelcernkovic/Documents/School/COP4870/DatabaseFolder";
            _clientRoot = $"{_root}/Clients";
            _projectRoot = $"{_root}/Projects";
            _billRoot = $"{_root}/Bills";
            _employeeRoot = $"{_root}/Employees";
            _timeRoot = $"{_root}/Times";

            //Init Hard Coded Data
            //List<Client> clients = new List<Client>
            //{
            //    new Client { Id = 1, Name = "Elon Musk", IsActive = true,       OpenDate = new DateTime(2019,05,09,0,0,0), Notes = "Super rich Billionaire."},
            //    new Client { Id = 2, Name = "John Paul", IsActive = false,      OpenDate = new DateTime(2020,03,10,9,0,0), Notes = "A pirate maybe"},
            //    new Client { Id = 3, Name = "Jack Sparrow", IsActive = true,    OpenDate = new DateTime(2015,06,22,1,0,0), Notes = "The Real pirate"},
            //    new Client { Id = 4, Name = "DOD", IsActive = true,             OpenDate = new DateTime(2012,09,06,2,0,0), Notes = "Government Department of Defense."}
            //};

            //List<Project> projects = new List<Project>
            //{
            //    new Project { Id = 1, Name = "Clean Room",       IsActive = true, ClientId = 1, OpenDate = new DateTime(2019,05,30,0,0,0), Notes = "Random Note"},
            //    new Project { Id = 2, Name = "Work on Homework", IsActive = true, ClientId = 2, CloseDate = new DateTime(2012,06,20,0,0,0), Notes = "Two assignments due"},
            //    new Project { Id = 3, Name = "Build Spaceship", IsActive = true, ClientId = 3, CloseDate = new DateTime(2013, 07, 09, 0, 0, 0), Notes = "Watch the terminator" },
            //    new Project { Id = 4, Name = "Fix the black pearl", IsActive = true, ClientId = 1, OpenDate = new DateTime(2016, 08, 09, 0, 0, 0), Notes = "This is another note" },
            //    new Project { Id = 5, Name = "Feed the dog", IsActive = true, ClientId = 3, OpenDate = new DateTime(2010, 09, 09, 0, 0, 0), Notes = "One more note" },
            //    new Project { Id = 6, Name = "Random Project", IsActive = true, ClientId = 4, CloseDate = new DateTime(2029, 02, 10, 0, 0, 0), Notes = "I am tired of writing notes" },
            //    new Project { Id = 7, Name = "Workout", IsActive = true, ClientId = 4, OpenDate = new DateTime(2040, 01, 04, 0, 0, 0), Notes = "The new spiderman movie is out" },
            //    new Project { Id = 8, Name = "COP4870 Proj", IsActive = true, ClientId = 2, OpenDate = new DateTime(2013, 01, 03, 0, 0, 0), Notes = "Fool me twice shame" }
            //};

            //List<Bill> bills = new List<Bill>
            //{
            //    new Bill { Id = 2, TotalAmount=123, IsActive = true, ClientId = 1, ProjectId = 1, DueDate = new DateTime(2012,06,20,0,0,0)},
            //    new Bill { Id = 3, TotalAmount=233, IsActive = true, ClientId = 1, ProjectId = 1, DueDate = new DateTime(2013,07,09,0,0,0)},
            //    new Bill { Id = 4, TotalAmount=425, IsActive = true, ClientId = 1, ProjectId = 1, DueDate = new DateTime(2016, 08, 09, 0, 0, 0) },
            //    new Bill { Id = 5, TotalAmount=898, IsActive = true, ClientId = 2, ProjectId = 1, DueDate = new DateTime(2010, 09, 09, 0, 0, 0) },
            //    new Bill { Id = 6, TotalAmount=450, IsActive = true, ClientId = 2, ProjectId = 1, DueDate = new DateTime(2029, 02, 10, 0, 0, 0) }
            //};

            //List<Employee> employees = new List<Employee>
            //{
            //    new Employee { Id = 1, Name = "Milan", PayRate = 0.90},
            //    new Employee { Id = 2, Name = "Mike", PayRate = 0.40},
            //    new Employee { Id = 3, Name = "Alex", PayRate = 0.70 },
            //    new Employee { Id = 4, Name = "Katie", PayRate = 0.30 },
            //    new Employee { Id = 5, Name = "Christina", PayRate = 0.80 }
            //};

            //List<Time> times = new List<Time>
            //{
            //    new Time { Id = 1, BillId = 2, Hours=0, EmployeeId=2, Narrative="Brushed teeth"},
            //    new Time { Id = 2, BillId = 2, Hours=0, EmployeeId=5, Narrative="Finished Homework"},
            //    new Time { Id = 3, BillId = 2, Hours = 0, EmployeeId = 2, Narrative = "Walked the dog" },
            //    new Time { Id = 4, BillId = 1, Hours = 0, EmployeeId = 3, Narrative = "Got Milk" }
            //};

            //foreach (Client c in clients)
            //{
            //    AddOrUpdate(c);
            //}

            //foreach (Project p in projects)
            //{
            //    AddOrUpdate(p);
            //}

            //foreach (Bill b in bills)
            //{
            //    AddOrUpdate(b);
            //}

            //foreach (Employee e in employees)
            //{
            //    AddOrUpdate(e);
            //}

            //foreach (Time t in times)
            //{
            //    AddOrUpdate(t);
            //}
        }

        private int LastClientId => Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;
        public int LastProjectId => Projects.Any() ? Projects.Select(c => c.Id).Max() : 0;
        public int LastBillId => Bills.Any() ? Bills.Select(c => c.Id).Max() : 0;
        public int LastEmployeeId => Employees.Any() ? Employees.Select(c => c.Id).Max() : 0;
        public int LastTimeId => Times.Any() ? Times.Select(c => c.Id).Max() : 0;


        public List<Client> Clients
        {
            get
            {
                var root = new DirectoryInfo(_clientRoot);
                var _clients = new List<Client>();
                //var ls = root.GetFiles();
                foreach (var todoFile in root.GetFiles())
                {
                    var todo = JsonConvert.
                        DeserializeObject<Client>
                        (File.ReadAllText(todoFile.FullName));
                    if (todo != null)
                    {
                        _clients.Add(todo);
                    }
                }
                return _clients;
            }
        }

        public List<Project> Projects
        {
            get
            {
                var root = new DirectoryInfo(_projectRoot);
                var _projects = new List<Project>();
                foreach (var todoFile in root.GetFiles())
                {
                    var todo = JsonConvert.
                        DeserializeObject<Project>
                        (File.ReadAllText(todoFile.FullName));

                    if (todo != null)
                    {
                        _projects.Add(todo);
                    }
                }
                return _projects;
            }
        }

        public List<Bill> Bills
        {
            get
            {
                var root = new DirectoryInfo(_billRoot);
                var _bills = new List<Bill>();
                foreach (var todoFile in root.GetFiles())
                {
                    var todo = JsonConvert.
                        DeserializeObject<Bill>
                        (File.ReadAllText(todoFile.FullName));

                    if (todo != null)
                    {
                        _bills.Add(todo);
                    }
                }
                return _bills;
            }
        }

        public List<Employee> Employees
        {
            get
            {
                var root = new DirectoryInfo(_employeeRoot);
                var _employees = new List<Employee>();
                foreach (var todoFile in root.GetFiles())
                {
                    var todo = JsonConvert.
                        DeserializeObject<Employee>
                        (File.ReadAllText(todoFile.FullName));

                    if (todo != null)
                    {
                        _employees.Add(todo);
                    }
                }
                return _employees;
            }
        }

        public List<Time> Times
        {
            get
            {
                var root = new DirectoryInfo(_timeRoot);
                var _times = new List<Time>();
                foreach (var todoFile in root.GetFiles())
                {
                    var todo = JsonConvert.
                        DeserializeObject<Time>
                        (File.ReadAllText(todoFile.FullName));

                    if (todo != null)
                    {
                        _times.Add(todo);
                    }
                }
                return _times;
            }
        }


        public Client AddOrUpdate(Client c)
        {
            //set up a new Id if one doesn't already exist
            if (c.Id <= 0)
            {
                c.Id = LastClientId + 1;
            }

            var path = $"{_clientRoot}/{c.Id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(c));

            //return the item, which now has an id
            return c;
        }

        public Project AddOrUpdate(Project p)
        {
            //set up a new Id if one doesn't already exist
            if (p.Id <= 0)
            {
                p.Id = LastProjectId + 1;
            }

            var path = $"{_projectRoot}/{p.Id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(p));

            //return the item, which now has an id
            return p;
        }

        public Bill AddOrUpdate(Bill b)
        {
            //set up a new Id if one doesn't already exist
            if (b.Id <= 0)
            {
                b.Id = LastBillId + 1;
            }

            var path = $"{_billRoot}/{b.Id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(b));

            //return the item, which now has an id
            return b;
        }

        public Employee AddOrUpdate(Employee e)
        {
            //set up a new Id if one doesn't already exist
            if (e.Id <= 0)
            {
                e.Id = LastEmployeeId + 1;
            }

            var path = $"{_employeeRoot}/{e.Id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(e));

            //return the item, which now has an id
            return e;
        }

        public Time AddOrUpdate(Time t)
        {
            //set up a new Id if one doesn't already exist
            if (t.Id <= 0)
            {
                t.Id = LastTimeId + 1;
            }

            var path = $"{_timeRoot}/{t.Id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(t));

            //return the item, which now has an id
            return t;
        }


        public bool DeleteClient(string id)
        {
            var path = $"{_clientRoot}/{id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }
            return true;
        }

        public bool DeleteProject(string id)
        {
            var path = $"{_projectRoot}/{id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }
            return true;
        }

        public bool DeleteBill(string id)
        {
            var path = $"{_billRoot}/{id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }
            return true;
        }

        public bool DeleteEmployee(string id)
        {
            var path = $"{_employeeRoot}/{id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }
            return true;
        }

        public bool DeleteTime(string id)
        {
            var path = $"{_timeRoot}/{id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }
            return true;
        }
    }
}

