using Microsoft.EntityFrameworkCore;

namespace TestTask.Models
{
    public class FolderContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }

        public FolderContext(DbContextOptions<FolderContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }
}
