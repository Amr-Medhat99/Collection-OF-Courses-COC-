﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using coc_graduation_project_.Models;

namespace coc_graduation_project_.Migrations
{
    [DbContext(typeof(DBcontext))]
    [Migration("20210607181100_Make-MTM-Between-PackageTable-And-CartTable---Make-MTM-Between-PackageTable-And-CurrentCourseTable")]
    partial class MakeMTMBetweenPackageTableAndCartTableMakeMTMBetweenPackageTableAndCurrentCourseTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BranchAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CenterId")
                        .HasColumnType("int");

                    b.HasKey("BranchId");

                    b.HasIndex("CenterId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Center", b =>
                {
                    b.Property<int>("CenterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CenterName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CenterId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Centers");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CommentBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MediaID")
                        .HasColumnType("int");

                    b.HasKey("CommentID");

                    b.HasIndex("MediaID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Component", b =>
                {
                    b.Property<int>("ComponentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ComponentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumOfVideos")
                        .HasColumnType("int");

                    b.Property<int>("PackageID")
                        .HasColumnType("int");

                    b.Property<double>("Prise")
                        .HasColumnType("float");

                    b.HasKey("ComponentID");

                    b.HasIndex("PackageID");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CenterID")
                        .HasColumnType("int");

                    b.Property<string>("CourseDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseRequire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstructorID")
                        .HasColumnType("int");

                    b.Property<int>("SubCategoryID")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.HasIndex("CenterID");

                    b.HasIndex("InstructorID");

                    b.HasIndex("SubCategoryID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.CoursePackage", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("PackageID")
                        .HasColumnType("int");

                    b.HasKey("CartId", "PackageID");

                    b.HasIndex("PackageID");

                    b.ToTable("CourseCarts");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.CurrentCourse", b =>
                {
                    b.Property<int>("CurrentCourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("studentID")
                        .HasColumnType("int");

                    b.HasKey("CurrentCourseID");

                    b.HasIndex("studentID")
                        .IsUnique();

                    b.ToTable("CurrentCourses");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.CurrentCoursePackage", b =>
                {
                    b.Property<int>("CurrentCourseID")
                        .HasColumnType("int");

                    b.Property<int>("PackageID")
                        .HasColumnType("int");

                    b.HasKey("CurrentCourseID", "PackageID");

                    b.HasIndex("PackageID");

                    b.ToTable("CurrentCourseCourses");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Favorite", b =>
                {
                    b.Property<int>("FavoriteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("studentID")
                        .HasColumnType("int");

                    b.HasKey("FavoriteID");

                    b.HasIndex("studentID")
                        .IsUnique();

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.FavoriteSubCategory", b =>
                {
                    b.Property<int>("FavoriteID")
                        .HasColumnType("int");

                    b.Property<int>("SubCategoryID")
                        .HasColumnType("int");

                    b.HasKey("FavoriteID", "SubCategoryID");

                    b.HasIndex("SubCategoryID");

                    b.ToTable("FavoriteCourses");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Instructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("InstructorId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.MainCategory", b =>
                {
                    b.Property<int>("MainCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("MainCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MainCategoryID");

                    b.ToTable("MainCategorys");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Media", b =>
                {
                    b.Property<int>("MediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("MediaLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MediaTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MediaId");

                    b.HasIndex("CourseId");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Package", b =>
                {
                    b.Property<int>("PackageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("PackageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PackagePrise")
                        .HasColumnType("float");

                    b.HasKey("PackageID");

                    b.HasIndex("CourseID");

                    b.ToTable("Package");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.QAndAVideo", b =>
                {
                    b.Property<int>("QAndAVideoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MediaID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QAndAVideoID");

                    b.HasIndex("MediaID");

                    b.ToTable("QandAVideos");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StudentId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("MainCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("SubCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubCategoryID");

                    b.HasIndex("MainCategoryID");

                    b.ToTable("SubCategorys");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.WatchLater", b =>
                {
                    b.Property<int>("WatchLaterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("studentID")
                        .HasColumnType("int");

                    b.HasKey("WatchLaterId");

                    b.HasIndex("studentID")
                        .IsUnique();

                    b.ToTable("watchLaters");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.WatchLaterCourse", b =>
                {
                    b.Property<int>("WatchLaterID")
                        .HasColumnType("int");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.HasKey("WatchLaterID", "CourseID");

                    b.HasIndex("CourseID");

                    b.ToTable("WatchLaterCourses");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Branch", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Center", "Center")
                        .WithMany()
                        .HasForeignKey("CenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Center");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Cart", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Student", "Student")
                        .WithOne("cart")
                        .HasForeignKey("coc_graduation_project_.Models.Cart", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Center", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Comment", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Media", "Media")
                        .WithMany()
                        .HasForeignKey("MediaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Media");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Component", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Package", "Package")
                        .WithMany()
                        .HasForeignKey("PackageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Course", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Center", "Center")
                        .WithMany()
                        .HasForeignKey("CenterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("coc_graduation_project_.Models.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("coc_graduation_project_.Models.SubCategory", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Center");

                    b.Navigation("Instructor");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.CoursePackage", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Cart", "Cart")
                        .WithMany("CourseCart")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("coc_graduation_project_.Models.Package", "Package")
                        .WithMany("CoursePackage")
                        .HasForeignKey("PackageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.CurrentCourse", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Student", "student")
                        .WithOne("CurrentCourse")
                        .HasForeignKey("coc_graduation_project_.Models.CurrentCourse", "studentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("student");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.CurrentCoursePackage", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.CurrentCourse", "CurrentCourse")
                        .WithMany("CurrentCourseCourse")
                        .HasForeignKey("CurrentCourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("coc_graduation_project_.Models.Package", "Package")
                        .WithMany("CurrentCoursePackage")
                        .HasForeignKey("PackageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentCourse");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Favorite", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Student", "student")
                        .WithOne("Favorite")
                        .HasForeignKey("coc_graduation_project_.Models.Favorite", "studentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("student");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.FavoriteSubCategory", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Favorite", "Favorite")
                        .WithMany("FavoriteSubCategory")
                        .HasForeignKey("FavoriteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("coc_graduation_project_.Models.SubCategory", "SubCategory")
                        .WithMany("FavoriteSubCategory")
                        .HasForeignKey("SubCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Favorite");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Instructor", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Media", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Package", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.QAndAVideo", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Media", "Media")
                        .WithMany()
                        .HasForeignKey("MediaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Media");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Student", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.SubCategory", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.MainCategory", "MainCategory")
                        .WithMany()
                        .HasForeignKey("MainCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainCategory");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.WatchLater", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Student", "student")
                        .WithOne("watchLater")
                        .HasForeignKey("coc_graduation_project_.Models.WatchLater", "studentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("student");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.WatchLaterCourse", b =>
                {
                    b.HasOne("coc_graduation_project_.Models.Course", "Course")
                        .WithMany("WatchLaterCourse")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("coc_graduation_project_.Models.WatchLater", "WatchLater")
                        .WithMany("WatchLaterCourse")
                        .HasForeignKey("WatchLaterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("WatchLater");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Cart", b =>
                {
                    b.Navigation("CourseCart");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Course", b =>
                {
                    b.Navigation("WatchLaterCourse");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.CurrentCourse", b =>
                {
                    b.Navigation("CurrentCourseCourse");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Favorite", b =>
                {
                    b.Navigation("FavoriteSubCategory");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Package", b =>
                {
                    b.Navigation("CoursePackage");

                    b.Navigation("CurrentCoursePackage");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.Student", b =>
                {
                    b.Navigation("cart");

                    b.Navigation("CurrentCourse");

                    b.Navigation("Favorite");

                    b.Navigation("watchLater");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.SubCategory", b =>
                {
                    b.Navigation("FavoriteSubCategory");
                });

            modelBuilder.Entity("coc_graduation_project_.Models.WatchLater", b =>
                {
                    b.Navigation("WatchLaterCourse");
                });
#pragma warning restore 612, 618
        }
    }
}