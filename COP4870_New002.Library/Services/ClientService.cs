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
                new Client { Id = 1, Name = "some name", IsActive = true},
                new Client { Id = 2, Name = "noah bear", IsActive = false},
                new Client { Id = 3, Name="Noah", IsActive = true},
                new Client { Id = 4, Name = "Mom", IsActive = true},
                new Client { Id = 5, Name = "Papa", IsActive = false},
                new Client { Id = 6, Name = "Katie", IsActive = true},
                new Client { Id = 7, Name = "Christina", IsActive = true},
                new Client { Id = 8, Name = "Jack", IsActive = false},
                new Client { Id = 9, Name = "Abby", IsActive = true}
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
