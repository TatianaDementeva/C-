using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RucksackProblem
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Rucksack> Rucksacks { get; set; }
        public DbSet<Thing> Things { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=rucksackproblemdb;Trusted_Connection=True;");
        }
    }


}
