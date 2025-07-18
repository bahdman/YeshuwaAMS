﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using src.Enums;
using src.Models;

namespace src.Data
{
    public class IdentityDataInitializer
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Models.User>>();

            string[] roleNames = { "Student", "Lecturer" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Seed Lecturer
            var lecturer = await userManager.FindByNameAsync("lecturer1");
            if (lecturer == null)
            {
                var newLecturer = new User
                {
                    UserName = "lecturer1",
                    Email = "lecturer1@example.com",
                    FullName = "Default Lecturer",
                    Gender = Gender.Male,
                    DOB = new DateOnly(1980, 1, 1),
                    Nationality = "Nigeria",
                    StateOfOrigin = "Lagos",
                    LGA = "Ikeja",
                    Program = string.Empty,
                    EntryMode = string.Empty,
                    Level = Level.five, //shouldn't be but cause of rush hour :)

                    // DepartmentId = 1 // Optional: depends on if seeded
                };

                var result = await userManager.CreateAsync(newLecturer, "Lecturer123@");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(newLecturer, "Lecturer");
            }
        }

        public static async Task SeedInvoiceAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!await context.Invoices.AnyAsync())
            {
                var invoices = new List<Invoice>
                {
                    new Invoice { FeeName = "Tuition Fee", Amount = 50000, Installments = 2 },
                    new Invoice { FeeName = "Hostel Fee", Amount = 30000, Installments = 1 },
                    new Invoice { FeeName = "Library Fee", Amount = 10000, Installments = 1 },
                    new Invoice { FeeName = "ICT Fee", Amount = 15000, Installments = 1 },
                    new Invoice { FeeName = "Medical Fee", Amount = 8000, Installments = 1 }
                };

                context.Invoices.AddRange(invoices);
                await context.SaveChangesAsync();
            }       
        }
    }
}
