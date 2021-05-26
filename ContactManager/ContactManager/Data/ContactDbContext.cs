using ContactManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager
{
    public class ContactDbContext : IdentityDbContext<User>
    {
        //Contact DbSet
        public DbSet<Contact> Contacts { get; set; }

        //DbContext Constructor
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {

        }
        
        /// <summary>
        /// Seeding Users and Contacts with Fluent API
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.SeedUsers(modelBuilder);
            this.SeedContacts(modelBuilder);
        }
        
        /// <summary>
        /// Seeds Users
        /// </summary>
        private void SeedUsers(ModelBuilder builder)
        {
            //Create Roles
            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Regular User", NormalizedName = "REGULAR USER" }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed Users
            var passwordHasher = new PasswordHasher<User>();

            List<User> users = new List<User>()
            {
                new User {
                    FirstName = "Ayobami",
                    LastName = "Fadeni",
                    UserName = "fadeniayobami@gmail.com",
                    NormalizedUserName = "FADENIAYOBAMI@GMAIL.COM",
                    Email = "fadeniayobami@gmail.com",
                    NormalizedEmail = "FADENIAYOBAMI@GMAIL.COM",
                    SecurityStamp = Guid.NewGuid().ToString()
                },

                new User {
                    FirstName = "Fabian",
                    LastName = "Emmanuel",
                    UserName = "fabian12@gmail.com",
                    NormalizedUserName = "FABIAN12@GMAIL.COM",
                    Email = "fabian12@gmail.com",
                    NormalizedEmail = "FABIAN12@GMAIL.COM",
                    SecurityStamp = Guid.NewGuid().ToString()
                },
            };

            builder.Entity<User>().HasData(users);

            // Seed UserRoles
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "ayobammy1");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "fabian1");

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "Admin").Id
            });

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[1].Id,
                RoleId = roles.First(q => q.Name == "Regular User").Id
            });

            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }

        /// <summary>
        /// Seeds Contacts
        /// </summary>
        private void SeedContacts(ModelBuilder modelbuilder)
        {
            var contact1 = new Contact
            {
                Id = 1,
                FirstName = "Ayobami",
                LastName = "Fadeni",
                Email = "fadeniayobami@gmail.com",
                PhoneNumber = "+2348106322363"
            };
            var contact2 = new Contact
            {
                Id = 2,
                FirstName = "Afolabi",
                LastName = "Ahmed",
                Email = "afolabiahmed@gmail.com",
                PhoneNumber = "+2348135372863"
            };
            var contact3 = new Contact
            {
                Id = 3,
                FirstName = "Ibrahim",
                LastName = "Tope",
                Email = "ibrahimtope@gmail.com",
                PhoneNumber = "+2348165289045"
            };
            var contact4 = new Contact
            {
                Id = 4,
                FirstName = "Eugene",
                LastName = "Uche",
                Email = "eugeneuche@gmail.com",
                PhoneNumber = "+2348109873426"
            };
            var contact5 = new Contact
            {
                Id = 5,
                FirstName = "Victor",
                LastName = "Umeh",
                Email = "victorumeh@gmail.com",
                PhoneNumber = "+2348035871098"
            };
            var contact6 = new Contact
            {
                Id = 6,
                FirstName = "Olumide",
                LastName = "Joda",
                Email = "olumidejoda@gmail.com",
                PhoneNumber = "+2348062341790"
            };

            modelbuilder.Entity<Contact>().HasData(contact1);
            modelbuilder.Entity<Contact>().HasData(contact2);
            modelbuilder.Entity<Contact>().HasData(contact3);
            modelbuilder.Entity<Contact>().HasData(contact4);
            modelbuilder.Entity<Contact>().HasData(contact5);
            modelbuilder.Entity<Contact>().HasData(contact6);
        }

    }
}
