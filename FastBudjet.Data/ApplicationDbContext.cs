using FastBudjet.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastBudjet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountHistory> AccountHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(x => x.CategoryId).HasIdentityOptions(startValue: 31);
                entity.Property(x => x.Name);
                entity.HasOne(x => x.Parent)
                    .WithMany(x => x.Children)
                    .HasForeignKey(x => x.ParentId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Transaction>()
                .HasOne<Category>(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                ;

            modelBuilder.Entity<Transaction>()
                .HasOne<Account>(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                ;

            modelBuilder.Entity<AccountHistory>()
                .HasOne<Account>(ah => ah.Account)
                .WithMany(a => a.AccountHistories)
                .HasForeignKey(ah => ah.AccountId);

            modelBuilder.Entity<AccountHistory>()
                .HasOne<Transaction>(ah => ah.Transaction)
                .WithMany(t => t.AccountHistories)
                .HasForeignKey(ah => ah.TransactionId);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(x => x.Id).HasIdentityOptions(startValue: 4);
            });
                
        }
    }
}
