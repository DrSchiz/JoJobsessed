using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JoJobsessed.Models
{
    public partial class Clothes_Shop_DatabaseContext : DbContext
    {
        public Clothes_Shop_DatabaseContext()
        {
        }

        public Clothes_Shop_DatabaseContext(DbContextOptions<Clothes_Shop_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; } = null!;
        public virtual DbSet<ClothesShop> ClothesShops { get; set; } = null!;
        public virtual DbSet<ContactInformation> ContactInformations { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryProduct>(entity =>
            {
                entity.HasKey(e => e.IdCategoryProduct)
                    .HasName("ID_Category_Product");

                entity.ToTable("Category_Product");

                entity.Property(e => e.IdCategoryProduct).HasColumnName("ID_Category_Product");

                entity.Property(e => e.NameCategoryProduct)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Category_Product");
            });

            modelBuilder.Entity<ClothesShop>(entity =>
            {
                entity.HasKey(e => e.IdClothesShop)
                    .HasName("ID_Clothes_Shop");

                entity.ToTable("Clothes_Shop");

                entity.Property(e => e.IdClothesShop).HasColumnName("ID_Clothes_Shop");

                entity.Property(e => e.ContactInformationId).HasColumnName("Contact_Information_ID");

                entity.Property(e => e.NameClothesShop)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Clothes_Shop");

                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.ContactInformation)
                    .WithMany(p => p.ClothesShops)
                    .HasForeignKey(d => d.ContactInformationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_Information_Clothes_Shop");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ClothesShops)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Clothes_Shop");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ClothesShops)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Clothes_Shop");
            });

            modelBuilder.Entity<ContactInformation>(entity =>
            {
                entity.HasKey(e => e.IdContactInformation)
                    .HasName("ID_Contact_Information");

                entity.ToTable("Contact_Information");

                entity.Property(e => e.IdContactInformation).HasColumnName("ID_Contact_Information");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EMail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("E-Mail");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Number");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("ID_Product");

                entity.ToTable("Product");

                entity.Property(e => e.IdProduct).HasColumnName("ID_Product");

                entity.Property(e => e.CategoryProductId).HasColumnName("Category_Product_ID");

                entity.Property(e => e.NameProduct)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Product");

                entity.Property(e => e.PriceProduct).HasColumnName("Price_Product");

                entity.HasOne(d => d.CategoryProduct)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Product_Product");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("ID_User");

                entity.ToTable("User");

                entity.HasIndex(e => e.LoginUser, "UQ_Login_User")
                    .IsUnique();

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.EMailUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("E-Mail_User")
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.LoginUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Login_User");

                entity.Property(e => e.PasswordUser)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Password_User");

                entity.Property(e => e.PhoneNumberUser)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Number_User")
                    .HasDefaultValueSql("('-')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
