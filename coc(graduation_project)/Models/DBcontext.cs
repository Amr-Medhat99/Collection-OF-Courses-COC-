using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class DBcontext: IdentityDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one-to-one between student and cart
            modelBuilder.Entity<Student>()
            .HasOne(b => b.cart)
            .WithOne(i => i.Student)
            .HasForeignKey<Cart>(b => b.StudentId);

            //one-to-one between student and Favorite
            modelBuilder.Entity<Student>()
            .HasOne(b => b.Favorite)
            .WithOne(i => i.student)
            .HasForeignKey<Favorite>(b => b.studentID);


            //one-to-one between student and watchLater
            modelBuilder.Entity<Student>()
            .HasOne(p => p.watchLater)
            .WithOne(b => b.student)
            .HasForeignKey<WatchLater>(b => b.studentID);

            //one-to-one between student and CurrentCourse
            modelBuilder.Entity<Student>()
            .HasOne(p => p.CurrentCourse)
            .WithOne(b => b.student)
            .HasForeignKey<CurrentCourse>(b => b.studentID);

            //many-to-many between currentcourse and Package
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CurrentCoursePackage>().HasKey(u => new { u.CurrentCourseID, u.PackageID });
            modelBuilder.Entity<CurrentCoursePackage>()
                .HasOne(sc => sc.CurrentCourse)
                .WithMany(s => s.CurrentCourseCourse)
                .HasForeignKey(sc => sc.CurrentCourseID);

            modelBuilder.Entity<CurrentCoursePackage>()
               .HasOne(sc => sc.Package)
               .WithMany(s => s.CurrentCoursePackage)
               .HasForeignKey(sc => sc.PackageID);

            //many-to-many between Favorite and course
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoriteSubCategory>().HasKey(u => new { u.FavoriteID, u.SubCategoryID});
            modelBuilder.Entity<FavoriteSubCategory>()
                .HasOne(sc => sc.Favorite)
                .WithMany(s => s.FavoriteSubCategory)
                .HasForeignKey(sc => sc.FavoriteID);

            modelBuilder.Entity<FavoriteSubCategory>()
               .HasOne(sc => sc.SubCategory)
               .WithMany(s => s.FavoriteSubCategory)
               .HasForeignKey(sc => sc.SubCategoryID);

            //
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WatchLaterCourse>().HasKey(u => new { u.WatchLaterID, u.CourseID});
            modelBuilder.Entity<WatchLaterCourse>()
                .HasOne(sc => sc.WatchLater)
                .WithMany(s => s.WatchLaterCourse)
                .HasForeignKey(sc => sc.WatchLaterID);

            modelBuilder.Entity<WatchLaterCourse>()
               .HasOne(sc => sc.Course)
               .WithMany(s => s.WatchLaterCourse)
               .HasForeignKey(sc => sc.CourseID);

            //
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CoursePackage>().HasKey(u => new { u.CartId, u.PackageID });
            modelBuilder.Entity<CoursePackage>()
                .HasOne(sc => sc.Package)
                .WithMany(s => s.CoursePackage)
                .HasForeignKey(sc => sc.PackageID);

            modelBuilder.Entity<CoursePackage>()
               .HasOne(sc => sc.Cart)
               .WithMany(s => s.CourseCart)
               .HasForeignKey(sc => sc.CartId);



            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Cart> Carts{ get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CoursePackage> CoursePackage { get; set; }
        public DbSet<media> Medias { get; set; }
        public DbSet<WatchLater> watchLaters{ get; set; }
        public DbSet<WatchLaterCourse> WatchLaterCourses { get; set; }
        public DbSet<CurrentCourse> CurrentCourses { get; set; }
        public DbSet<CurrentCoursePackage> CurrentCoursePackage { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<FavoriteSubCategory> FavoriteSubCategory { get; set; }
        public DbSet<Comment>Comments { get; set; }
        public DbSet<QAndAVideo>QandAVideos{ get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<SubCategory> SubCategorys { get; set; }
        public DbSet<MainCategory> MainCategorys { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<FreeVideo> FreeVideos { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<HelpFullChannel> HelpFullChannels { get; set; }
        public DbSet<Admin> Admins{ get; set; }
        



        public DBcontext(DbContextOptions<DBcontext> options)
            : base(options)
        {
        }
    }
}
