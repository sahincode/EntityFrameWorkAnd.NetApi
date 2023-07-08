
using EntityFramework.DbData.Entities;
using Microsoft.EntityFrameworkCore;
namespace EntityFramework.DbData
{
    public class StudentEntityContext:DbContext
    {

        public StudentEntityContext(DbContextOptions<StudentEntityContext> options) : base(options) { }

        
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student>Students { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>().ToTable("tbl_Course");
            modelBuilder.Entity<Student>().ToTable("tbl_Student");
            modelBuilder.Entity<StudentCourse>().ToTable("StudentCourse");
            modelBuilder.Entity<StudentCourse>().HasKey(x => new { x.StudentID });

        }

    }
}
