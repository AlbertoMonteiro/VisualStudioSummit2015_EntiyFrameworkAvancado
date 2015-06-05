using System;
using System.Collections.Generic;
using System.Linq;
using NhaNhaNha.Modelos;
using VisualStudioSummitDemo.Models;

namespace VisualStudioSummitDemo.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DemoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DemoContext context)
        {
            var tenants = new[] { new Tenant { Id = 1, Name = "Empresa 1" }, new Tenant { Id = 2, Name = "Empresa 2" } };
            context.Tenants.AddOrUpdate(tenants);

            var stack = new Stack<int>(Enumerable.Range(1, 10).Select(x => Convert.ToInt32(x%2 == 0)));

            var random = new Random();

            for (int i = 0; i < 5; i++)
            {
                var homem = NhaNhaNha.NhaNhaNha.Homen;
                var contact = new Contact
                {
                    Name = homem.PrimeiroNome + " " + homem.SobreNome,
                    BirthDate = new DateTime(1990, random.Next(11) + 1, random.Next(27) + 1),
                    Genre = Genre.Male,
                    Tenant = tenants[stack.Pop()]
                };
                context.Contacts.AddOrUpdate(contact);
            }

            for (int i = 0; i < 5; i++)
            {
                var mulher = NhaNhaNha.NhaNhaNha.Mulher;
                var contact = new Contact
                {
                    Name = mulher.PrimeiroNome + " " + mulher.SobreNome,
                    BirthDate = new DateTime(1990, random.Next(11) + 1, random.Next(27) + 1),
                    Genre = Genre.Female,
                    Tenant = tenants[stack.Pop()]
                };
                context.Contacts.AddOrUpdate(contact);
            }
        }
    }
}
