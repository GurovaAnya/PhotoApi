﻿using System;
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
                .HasIndex(f => f.Photo);
            Seed(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-LOBOORA0\SQLEXPRESS;Initial Catalog=PhotoBase;Integrated Security=True");
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
                    Photo = new byte[] { 1,1,1,1,1},
                    PersonId = firstPerson.Id
                },
               new Face
               {
                   Id = -2,
                   Photo = new byte[] { 1, 0, 1, 0, 1, 1 },
                   PersonId = secondPerson.Id
               }           
            );
        }
    }
}
