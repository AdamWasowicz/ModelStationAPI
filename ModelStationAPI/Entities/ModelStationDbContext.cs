using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Entities
{
    public class ModelStationDbContext : DbContext
    {
        //private readonly string _ = @"Server=(localdb)\mssqllocaldb; Database=ModelStation; Trusted_Connection=True";

        public ModelStationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        //DbSets
        //Those Dbsets represents tables in database.
        //EntityFramework will create database by converting C# classes
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        
        //ModelBuilder
        //This method creates constraints on DbSets
        //for example, I can tell EntityFramework that User's Name
        //can be max 26 characters and is required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            

            //Comments
            modelBuilder.Entity<Comment>()
                .Property(p => p.IsActive)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(p => p.IsBanned)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(p => p.Text)
                .HasMaxLength(256)
                .IsRequired();


            //Posts
            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                .HasMaxLength(64)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.Text)
                .HasMaxLength(256);

            modelBuilder.Entity<Post>()
                .Property(p => p.IsActive)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.IsBanned)
                .IsRequired();


            //PostCategories
            modelBuilder.Entity<PostCategory>()
                .Property(p => p.Name)
                .HasMaxLength(64)
                .IsRequired();

            modelBuilder.Entity<PostCategory>()
                .Property(p => p.Description)
                .HasMaxLength(256)
                .IsRequired();


            //Users
            modelBuilder.Entity<User>()
                .Property(p => p.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(p => p.Name)
                .HasMaxLength(64);

            modelBuilder.Entity<User>()
                .Property(p => p.Surname)
                .HasMaxLength(64);

            modelBuilder.Entity<User>()
                .Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(64);

            modelBuilder.Entity<User>()
                .Property(p => p.Description)
                .HasMaxLength(256);

            modelBuilder.Entity<User>()
                .Property(p => p.IsActive)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(p => p.IsBanned)
                .IsRequired();


        }

        
        //This where I set database that will be used by EntityFramework
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //}
    }
}
