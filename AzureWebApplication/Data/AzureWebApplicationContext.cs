using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AzureWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AzureWebApplication.Data
{
    public class AzureWebApplicationContext : IdentityDbContext<IdentityUser> {
        public AzureWebApplicationContext (DbContextOptions<AzureWebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<AzureWebApp.Models.Computer> Computer { get; set; } = default!;
    }
}
