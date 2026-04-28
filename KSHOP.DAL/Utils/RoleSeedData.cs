using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Utils
{
    public class RoleSeedData : ISeedData
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleSeedData(RoleManager<IdentityRole> roleManager)
        {
            _roleManager=roleManager;


        }
        public async Task DataSeed()
        {
            Console.WriteLine("Seeding Roles Started...");

            string[] roles = { "Admin", "User", "SuperAdmin" };

            foreach (var role in roles)
            {
                Console.WriteLine($"Checking role: {role}");

                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(role));

                    if (result.Succeeded)
                        Console.WriteLine($"Created role: {role}");
                    else
                    {
                        Console.WriteLine($"Failed to create role: {role}");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description);
                        }
                    }
                }
            }
        }


    }
}
