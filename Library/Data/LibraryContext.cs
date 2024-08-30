using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity;

//using System.Data.Entity.ModelConfiguration.Conventions;

namespace Library.Data
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        
        public DbSet<Book> Books { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Year> Years { get; set; }

        public DbSet<Publisher> Publishers { get; set; }
        

        public DbSet<User> Users { get; set; } = null!;

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("InstructorID")
                    .ToTable("CourseInstructor"));
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountId).HasName("PK_Accounts_1");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.Property(e => e.Created).HasColumnType("datetime");
                entity.Property(e => e.CreatedByIp).HasMaxLength(50);
                entity.Property(e => e.Expires).HasColumnType("datetime");
                entity.Property(e => e.ReasonRevoked).HasMaxLength(50);
                entity.Property(e => e.Revoked).HasColumnType("datetime");
                entity.Property(e => e.RevokedByIp).HasMaxLength(50);

                entity.HasOne(d => d.Accounts).WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RefreshTo__UserI__65370702");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);*/
    }
}
