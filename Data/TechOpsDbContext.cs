using Microsoft.EntityFrameworkCore;
using TechOpsDashboard.Models;

namespace TechOpsDashboard.Data
{
    public class TechOpsDbContext : DbContext
    {
        public TechOpsDbContext(DbContextOptions<TechOpsDbContext> options)
            : base(options)
        {
        }

        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
    }
}