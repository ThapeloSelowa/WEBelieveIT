using Microsoft.EntityFrameworkCore;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = ModelLibrary.Models.Task;

namespace TaskManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TeamMember_Hours_Work> TeamMember_Hours_Work { get; set; }
        public DbSet<Task_TeamMember> Task_TeamMember { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<System_Audits> System_Audits { get; set; }
        public DbSet<ErrorLog> Error_Logs { get; set; }
    }
}
