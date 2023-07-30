using System;
using COP4870_New002.Library.DTO;
using COP4870_New002.Library.Models;
using COP4870.API.Database;
using COP4870_New002.Library.Services;

namespace COP4870.API.EC
{
	public class ClientEC
	{
        public List<Client> Search(string query = "")
        {
            if(query == "")
            {
                return Filebase.Current.Clients;
            }
            return Filebase.Current.Clients
                .Where(c => c.Name.ToUpper()
                .Contains(query.ToUpper())).ToList();
        }

        public ClientDTO? Get(int id)
        {
            var cli = Filebase.Current.Clients
                 .FirstOrDefault(c => c.Id == id)
                 ?? new Client();

            var projs = Filebase.Current.Projects
                .Where(b => b.ClientId == cli.Id).ToList()
                ?? new List<Project>();

            var bills = Filebase.Current.Bills
                .Where(b => b.ClientId == cli.Id).ToList()
                ?? new List<Bill>();

            return new ClientDTO(cli, projs, bills);
        }

        public void AddOrUpdate(Client c)
        {
            Filebase.Current.AddOrUpdate(c);
        }

        public void Delete(int id)
        {
            var pl = Filebase.Current.Projects.Where(b => b.ClientId == id).ToList();
            foreach (Project proj in pl)
            {
                proj.ClientId = 0;
                Filebase.Current.AddOrUpdate(proj);
            }

            var bl = Filebase.Current.Bills.Where(b => b.ClientId == id).ToList();
            foreach (Bill bill in bl)
            {
                bill.ClientId = 0;
                Filebase.Current.AddOrUpdate(bill);
            }
            Filebase.Current.DeleteClient(id.ToString());
        }
    }
}

