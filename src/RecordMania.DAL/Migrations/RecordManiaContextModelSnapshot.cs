using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RecordMania.DAL;


#nullable disable

namespace RecordMania.DAL.Migrations
{
    [DbContext(typeof(RecordManiaContext))]
    partial class RecordManiaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("RecordMania.DAL.Models.CodeTask", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                b.Property<string>("Description").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<string>("Name").IsRequired().HasColumnType("nvarchar(max)");
                b.HasKey("Id");
                b.ToTable("Tasks"); // or "CodeTasks" if renamed via annotation or Fluent API
            });

            modelBuilder.Entity("RecordMania.DAL.Models.ProgrammingLanguage", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                b.Property<string>("Name").IsRequired().HasColumnType("nvarchar(max)");
                b.HasKey("Id");
                b.ToTable("Languages");
            });

            modelBuilder.Entity("RecordMania.DAL.Models.Student", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                b.Property<string>("Email").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<string>("FirstName").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<string>("LastName").IsRequired().HasColumnType("nvarchar(max)");
                b.HasKey("Id");
                b.ToTable("Students");
            });

            modelBuilder.Entity("RecordMania.DAL.Models.Record", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                b.Property<DateTime>("Created").HasColumnType("datetime2");
                b.Property<double>("ExecutionTime").HasColumnType("float");
                b.Property<int>("ProgrammingLanguageId").HasColumnType("int");
                b.Property<int>("StudentId").HasColumnType("int");
                b.Property<int>("TaskId").HasColumnType("int");
                b.HasKey("Id");
                b.HasIndex("ProgrammingLanguageId");
                b.HasIndex("StudentId");
                b.HasIndex("TaskId");
                b.ToTable("Records");
            });

            modelBuilder.Entity("RecordMania.DAL.Models.Record", b =>
            {
                b.HasOne("RecordMania.DAL.Models.ProgrammingLanguage", "ProgrammingLanguage")
                    .WithMany("Records")
                    .HasForeignKey("ProgrammingLanguageId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("RecordMania.DAL.Models.Student", "Student")
                    .WithMany("Records")
                    .HasForeignKey("StudentId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("RecordMania.DAL.Models.CodeTask", "CodeTask")
                    .WithMany("Records")
                    .HasForeignKey("TaskId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("ProgrammingLanguage");
                b.Navigation("Student");
                b.Navigation("CodeTask");
            });
        }
    }
}
