using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZenithWebsite.Data;
using ZenithWebsite.Models;

namespace ZenithWebsite.Models
{
    public class SeedData
    {
        public async static void Initialize(ApplicationDbContext db, IServiceProvider serviceProvider)
        {
            AddActivities(db);
            AddEvents(db);
            await AddUserAndRoles(serviceProvider);
        }

        public static async Task AddUserAndRoles(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = new string[] { "Member", "Admin" };

            foreach (string role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            ApplicationUser admin = new ApplicationUser() {
                Email = "a@a.a",
                UserName = "a"};
            ApplicationUser member = new ApplicationUser() {
                Email = "m@m.m",
                UserName = "m" };

            await userManager.CreateAsync(admin, "P@$$w0rd");
            await userManager.CreateAsync(member, "P@$$w0rd");

            context.SaveChanges();

            await userManager.AddToRoleAsync(context.Users.Where(u => u.UserName == "a").FirstOrDefault(), "Admin");
            await userManager.AddToRoleAsync(context.Users.Where(u => u.UserName == "a").FirstOrDefault(), "Member");
            await userManager.AddToRoleAsync(context.Users.Where(u => u.UserName == "m").FirstOrDefault(), "Member");

            context.SaveChanges();
        }

        public static void AddEvents(ApplicationDbContext db)
        {
            if(!db.Events.Any())
            {
                db.Events.Add(
                     new Event
                     {
                         ActivityId = 1,
                         Activity = db.Activities.First(a => a.ActivityDescription == "Senior’s  Golf Tournament"),
                         EventFrom = new DateTime(2017, 4, 4, 8, 30, 0),
                         EventTo = new DateTime(2017, 4, 4, 10, 30, 0),
                         CreatedBy = "a",
                         CreationDate = DateTime.Now,
                         IsActive = true
                     }
                );
                db.Events.Add(
                    new Event
                    {
                        ActivityId = 2,
                        Activity = db.Activities.First(a => a.ActivityDescription == "Leadership General Assembly Meeting"),
                        EventFrom = new DateTime(2017, 4, 5, 8, 30, 0),
                        EventTo = new DateTime(2017, 4, 5, 10, 30, 0),
                        CreatedBy = "a",
                        CreationDate = DateTime.Now,
                        IsActive = true
                    }
               );
                db.Events.Add(
                     new Event
                     {
                         ActivityId = 3,
                         Activity = db.Activities.First(a => a.ActivityDescription == "Youth Bowling Tournament"),
                         EventFrom = new DateTime(2017, 4, 7, 17, 30, 0),
                         EventTo = new DateTime(2017, 4, 7, 19, 15, 0),
                         CreatedBy = "a",
                         CreationDate = DateTime.Now,
                         IsActive = true
                     }
                );
                db.Events.Add(
                     new Event
                     {
                         ActivityId = 4,
                         Activity = db.Activities.First(a => a.ActivityDescription == "Young Ladies Cooking Lessons"),
                         EventFrom = new DateTime(2017, 4, 7, 19, 0, 0),
                         EventTo = new DateTime(2017, 4, 7, 20, 0, 0),
                         CreatedBy = "a",
                         CreationDate = DateTime.Now,
                         IsActive = true
                     }
                );
                db.Events.Add(
                     new Event
                     {
                         ActivityId = 5,
                         Activity = db.Activities.First(a => a.ActivityDescription == "Youth Craft Lessons"),
                         EventFrom = new DateTime(2017, 4, 8, 8, 30, 0),
                         EventTo = new DateTime(2017, 4, 8, 10, 30, 0),
                         CreatedBy = "a",
                         CreationDate = DateTime.Now,
                         IsActive = true
                     }
                );
                db.Events.Add(
                     new Event
                     {
                         ActivityId = 6,
                         Activity = db.Activities.First(a => a.ActivityDescription == "Youth Choir Practice"),
                         EventFrom = new DateTime(2017, 4, 8, 10, 30, 0),
                         EventTo = new DateTime(2017, 4, 8, 12, 0, 0),
                         CreatedBy = "a",
                         CreationDate = DateTime.Now,
                         IsActive = true
                     }
                );
                db.Events.Add(
                     new Event
                     {
                         ActivityId = 7,
                         Activity = db.Activities.First(a => a.ActivityDescription == "Lunch"),
                         EventFrom = new DateTime(2017, 4, 8, 12, 0, 0),
                         EventTo = new DateTime(2017, 4, 8, 13, 30, 0),
                         CreatedBy = "a",
                         CreationDate = DateTime.Now,
                         IsActive = true
                     }
                );
                db.Events.Add(
                     new Event
                     {
                         ActivityId = 8,
                         Activity = db.Activities.First(a => a.ActivityDescription == "Pancake Breakfast"),
                         EventFrom = new DateTime(2017, 4, 9, 7, 30, 0),
                         EventTo = new DateTime(2017, 4, 9, 8, 30, 0),
                         CreatedBy = "a",
                         CreationDate = DateTime.Now,
                         IsActive = true
                     }
                );
                db.Events.Add(
                     new Event
                     {
                         ActivityId = 9,
                         Activity = db.Activities.First(a => a.ActivityDescription == "Youth Swimming Lessons"),
                         EventFrom = new DateTime(2017, 4, 9, 8, 30, 0),
                         EventTo = new DateTime(2017, 4, 9, 10, 30, 0),
                         CreatedBy = "a",
                         CreationDate = DateTime.Now,
                         IsActive = true
                     }
                );
                db.Events.Add(
                     new Event
                     {
                         ActivityId = 10,
                         Activity = db.Activities.First(a => a.ActivityDescription == "Parent Swimming Exercise"),
                         EventFrom = new DateTime(2017, 4, 9, 8, 30, 0),
                         EventTo = new DateTime(2017, 4, 9, 10, 30, 0),
                         CreatedBy = "a",
                         CreationDate = DateTime.Now,
                         IsActive = true
                     }
                );
                db.Events.Add(
                     new Event
                     {
                         ActivityId = 11,
                         Activity = db.Activities.First(a => a.ActivityDescription == "Bingo Tournament"),
                         EventFrom = new DateTime(2017, 4, 9, 10, 30, 0),
                         EventTo = new DateTime(2017, 4, 9, 12, 0, 0),
                         CreatedBy = "a",
                         CreationDate = DateTime.Now,
                         IsActive = true
                     }
                );
                db.Events.Add(
                     new Event
                     {
                         ActivityId = 12,
                         Activity = db.Activities.First(a => a.ActivityDescription == "BBQ Lunch"),
                         EventFrom = new DateTime(2017, 4, 9, 12, 0, 0),
                         EventTo = new DateTime(2017, 4, 9, 13, 0, 0),
                         CreatedBy = "a",
                         CreationDate = DateTime.Now,
                         IsActive = true
                     }
                );
                db.Events.Add(
                     new Event
                     {
                         ActivityId = 13,
                         Activity = db.Activities.First(a => a.ActivityDescription == "Garage Sale"),
                         EventFrom = new DateTime(2017, 4, 9, 13, 0, 0),
                         EventTo = new DateTime(2017, 4, 9, 18, 0, 0),
                         CreatedBy = "a",
                         CreationDate = DateTime.Now,
                         IsActive = true
                     }
                );
                db.SaveChanges();
            }
        }

        public static void AddActivities(ApplicationDbContext db)
        {
            if (!db.Activities.Any())
            {
                db.Activities.Add(
                    new Activity { ActivityDescription = "Senior’s  Golf Tournament", CreationDate = DateTime.Now }
                    );
                db.Activities.Add(
                    new Activity {  ActivityDescription = "Leadership General Assembly Meeting", CreationDate = DateTime.Now }
                    );
                db.Activities.Add(
                    new Activity { ActivityDescription = "Youth Bowling Tournament", CreationDate = DateTime.Now }
                    );
                db.Activities.Add(
                    new Activity { ActivityDescription = "Young Ladies Cooking Lessons", CreationDate = DateTime.Now }
                    );
                db.Activities.Add(
                    new Activity { ActivityDescription = "Youth Craft Lessons", CreationDate = DateTime.Now }
                    );
                db.Activities.Add(
                    new Activity { ActivityDescription = "Youth Choir Practice", CreationDate = DateTime.Now }
                    );
                db.Activities.Add(
                    new Activity { ActivityDescription = "Lunch", CreationDate = DateTime.Now }
                    );
                db.Activities.Add(
                    new Activity { ActivityDescription = "Pancake Breakfast", CreationDate = DateTime.Now }
                    );
                db.Activities.Add(
                    new Activity { ActivityDescription = "Youth Swimming Lessons", CreationDate = DateTime.Now }
                    );
                db.Activities.Add(
                    new Activity { ActivityDescription = "Parent Swimming Exercise", CreationDate = DateTime.Now }
                    );
                db.Activities.Add(
                    new Activity { ActivityDescription = "Bingo Tournament", CreationDate = DateTime.Now }
                    );
                db.Activities.Add(
                    new Activity { ActivityDescription = "BBQ Lunch", CreationDate = DateTime.Now }
                    );
                db.Activities.Add(
                    new Activity { ActivityDescription = "Garage Sale", CreationDate = DateTime.Now }
                    );
                db.SaveChanges();
            }
        }
    }
}
