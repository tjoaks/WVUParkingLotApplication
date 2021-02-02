using System;
using System.Collections.Generic;
using System.Text;
using DiscussionMVCAppOaks.Models;
using DiscussionMVCAppOaks.Models.LotModel;
using DiscussionMVCAppOaks.Models.LotTypeModel;
using DiscussionMVCAppOaks.Models.PermitModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiscussionMVCAppOaks.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Lot> Lots { get; set; }
        public DbSet<LotType> LotTypes { get; set; }
        public DbSet<LotStatus> LotStatuses { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<WVUEmployee> WVUEmployees { get; set; }
        public DbSet<Permit> Permits { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}