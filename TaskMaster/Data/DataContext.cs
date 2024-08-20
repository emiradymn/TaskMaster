using Microsoft.EntityFrameworkCore;
using TaskMaster.Data;

namespace TaskMaster.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Task> Tasks => Set<Task>();
        public DbSet<Stajyer> Stajyers => Set<Stajyer>();
        public DbSet<TaskSave> TaskSaves => Set<TaskSave>();
        public DbSet<Engineer> Engineers => Set<Engineer>();

    }
}