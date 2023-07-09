using COP4870_New002.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace COP4870_New002.Library.Services
{
    public class ClientService
    {
        private static ClientService? instance;
        private static object _lock = new object();

        public static ClientService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ClientService();
                    }
                }

                return instance;
            }
        }

        private List<Client> clients;
        private ClientService()
        {
            clients = new List<Client>
            {
                new Client { Id = 1, Name = "Elon Musk", IsActive = true,       OpenDate = new DateTime(2019,05,09,0,0,0), Notes = "Super rich Billionaire."},
                new Client { Id = 2, Name = "John Paul", IsActive = false,      OpenDate = new DateTime(2020,03,10,9,0,0), Notes = "A pirate maybe"},
                new Client { Id = 3, Name = "Jack Sparrow", IsActive = true,    OpenDate = new DateTime(2015,06,22,1,0,0), Notes = "The Real pirate"},
                new Client { Id = 4, Name = "DOD", IsActive = true,             OpenDate = new DateTime(2012,09,06,2,0,0), Notes = "Government Department of Defense."}
            };
        }

        public List<Client> Clients
        {
            get { return clients; }
        }

        public List<Client> SearchClients(string query)
        {
            return Clients.Where(s => s.Name.ToUpper()
            .Contains(query.ToUpper()))
            .ToList();
        }

        public Client? Get(int id)
        {
            return Clients.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Client? client)
        {
            if (client != null)
            {
                client.Id = clients.Last().Id + 1;
                clients.Add(client);
            }
        }

        public void Delete(int id)
        {
            var clientToRemove = Get(id);
            if (clientToRemove != null)
            {
                Clients.Remove(clientToRemove);
            }
        }

        public void Delete(Client s)
        {
            Delete(s.Id);
        }
    }
}
