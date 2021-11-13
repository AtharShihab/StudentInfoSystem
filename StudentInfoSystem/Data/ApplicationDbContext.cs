using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Models.Courses.Entities;
using StudentInfoSystem.Models.StudentCourses.Entities;
using StudentInfoSystem.Models.Students.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudentInfoSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> StudentInformation { get; set; }
        public DbSet<Course> CourseList { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);




            //soft delete
            builder.Entity<Student>().Property<bool>("isDeleted");
            builder.Entity<Student>().HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);

            builder.Entity<Course>().Property<bool>("isDeleted");
            builder.Entity<Course>().HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);

            builder.Entity<StudentCourse>().Property<bool>("isDeleted");
            builder.Entity<StudentCourse>().HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["isDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["isDeleted"] = true;
                        break;
                }
            }
        }


    }
}
