using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PhotoApi.Models
{
    public class PhotoDbContext:DbContext
    {
        public DbSet<Face> Faces { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Face>()
                .HasIndex(f => f.PhotoHash);
            Seed(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:photoapi.database.windows.net,1433;Initial Catalog=PhotoBase;Persist Security Info=False;User ID=anna;Password=qwerty123=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }


        private void Seed(ModelBuilder modelBuilder)
        {
            var firstPerson = new Person
            {
                Id = -1,
                FirstName = "Anna",
                LastName = "Gurova",
                Patronymic = "Aleksandrovna"
            };
            var secondPerson = new Person
            {
                Id = -2,
                FirstName = "Petr",
                LastName = "Petrov",
                Patronymic = "Petrovich"
            };
            modelBuilder.Entity<Person>().HasData(firstPerson, secondPerson);
            modelBuilder.Entity<Face>().HasData(
                new Face
                {
                    Id = -1,
                    PhotoHash = 1924905495,
                    PhotoName = "637360586020601329",
                    PersonId = firstPerson.Id
                },
               new Face
               {
                   Id = -2,
                   PhotoHash = 1429759506,
                   PhotoName = "637360612095630831",
                   PersonId = secondPerson.Id
               }           
            );
        }
    }
}
