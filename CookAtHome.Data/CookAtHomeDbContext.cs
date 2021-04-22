using CookAtHome.Data.Models;
using CookAtHome.Data.Models.Enum;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookAtHome.Data.Data
{
    public class CookAtHomeDbContext : IdentityDbContext<User>
    {
        public CookAtHomeDbContext(DbContextOptions<CookAtHomeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<RecipeTag> RecipeTags { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RecipeTag>().HasKey(rt => new { rt.RecipeId, rt.TagId });

            builder.Entity<RecipeTag>()
                .HasOne(r => r.Recipe)
                .WithMany(t => t.Tags)
                .HasForeignKey(r => r.RecipeId);

            builder.Entity<RecipeTag>()
                .HasOne(t => t.Tag)
                .WithMany(r => r.Recipes)
                .HasForeignKey(t => t.TagId);

            builder.Entity<User>()
                .HasMany(r => r.Recipes)
                .WithOne(u => u.Owner)
                .HasForeignKey(u => u.OwnerId);

            builder.Entity<User>()
               .HasMany(c => c.Comments)
               .WithOne(c => c.Owner)
               .HasForeignKey(c => c.OwnerId)
               .OnDelete(DeleteBehavior.Restrict);
            ;

            builder.Entity<Recipe>()
            .HasMany(c => c.Comments)
            .WithOne(c => c.Recipe)
            .HasForeignKey(c => c.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);
            ;

            builder.Entity<Recipe>()
                .Property(e => e.Category)
                .HasConversion<string>();

            builder.Entity<Recipe>()
                .Property(e => e.Level)
                .HasConversion<string>();

            base.OnModelCreating(builder);
        }
    }
}
