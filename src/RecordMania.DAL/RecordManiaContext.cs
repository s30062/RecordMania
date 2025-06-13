using Microsoft.EntityFrameworkCore;
using src.DAL.Models;
using src.RecordMania.DAL.Models;
using Task = System.Threading.Tasks.Task;
using Microsoft.EntityFrameworkCore;
namespace RecordMania.DAL
{
    public class RecordManiaContext : DbContext
    {
        public RecordManiaContext(DbContextOptions<RecordManiaContext> options) : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<ProgrammingLanguage> Languages => Set<ProgrammingLanguage>();
        public DbSet<CodeTask> Tasks => Set<CodeTask>(); 
        public DbSet<Record> Records => Set<Record>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Record>()
                .HasOne(r => r.CodeTask)
                .WithMany(t => t.Records)
                .HasForeignKey(r => r.TaskId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
