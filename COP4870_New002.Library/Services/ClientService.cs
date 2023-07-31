using COP4870_New002.Library.DTO;
using COP4870_New002.Library.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;
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
            UpdateClients();
        }

        public List<Client> Clients
        {
            get { return clients ?? new List<Client>(); }
        }

        public void UpdateClients()
        {
            var response = new WebRequestHandler()
                    .Get("/Client")
                    .Result;

            clients = JsonConvert
                .DeserializeObject<List<Client>>(response)
                ?? new List<Client>();
        }

        public List<Client> SearchClients(string query)
        {
            return Clients.Where(s => s.Name.ToUpper()
                        .Contains(query.ToUpper()))
                        .ToList();
        }

        public string GetName(int id)
        {
            var cli = Clients.FirstOrDefault(c => c.Id == id);
            if(cli == null)
            {
                return string.Empty;
            }
            return cli.Name;
        }

        public ClientDTO? Get(int id)
        {
            var response = new WebRequestHandler()
                    .Get($"/Client/{id}")
                    .Result;

            return JsonConvert.DeserializeObject
                <ClientDTO>(response) ?? new ClientDTO();
        }

        public void Add(Client? c)
        {
            if (c != null)
            {
                var response = new WebRequestHandler()
                    .Post("/Client/Add", c).Result;
                UpdateClients();
            }
        }

        public void Delete(Client s)
        {
            var response = new WebRequestHandler()
                .Delete($"/Client/Delete/{s.Id}").Result;
            UpdateClients();
        }
    }
}
