using Cooking.Service.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.Service.DAL
{
    public class CookingDbContext : DbContext
    {
        public CookingDbContext()
        {
        }
        public CookingDbContext(DbContextOptions<CookingDbContext> options) : base(options)
        {
        }

        public DbSet<IngredientAvailability> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Recipe> Recipe { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            //This is to demonstrate how you can intercept the tracker to for example do a soft delete or add auditing fields.
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable trackable)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.LastUpdatedAt = now;
                            break;

                        case EntityState.Added:
                            trackable.CreatedAt = now;
                            trackable.LastUpdatedAt = now;
                            break;

                        //I'm disabling the soft delete functionality for the sake of keeping things simple but this is to show how it can be done

                        //case EntityState.Deleted:
                        //    trackable.LastUpdatedAt = now;
                        //    trackable.IsDeleted = true;
                        //    entry.State = EntityState.Modified;
                        //    break;
                    }
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            //Create the recipes
            modelbuilder.Entity<Recipe>().HasData(new Recipe
            {
                Id = 1,
                Name = "Burger",
                Portions = 1,
                //Again overkill
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                IsDeleted = false
            });

            //Add Recipe Ingredients
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 1,
                    //FK
                    RecipeId = 1,
                    Name = "Meat",
                    Amount = 1,
                    //Overkill but what the hell... :D
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 2,
                    RecipeId = 1,
                    Name = "Lettuce",
                    Amount = 1,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 3,
                    RecipeId = 1,
                    Name = "Tomato",
                    Amount = 1,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 4,
                    RecipeId = 1,
                    Name = "Cheese",
                    Amount = 1,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 5,
                    RecipeId = 1,
                    Name = "Dough",
                    Amount = 1,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });

            modelbuilder.Entity<Recipe>().HasData(new Recipe
            {
                Id = 2,
                Name = "Pie",
                Portions = 1,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                IsDeleted = false
            });

            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 6,
                    RecipeId = 2,
                    Name = "Dough",
                    Amount = 2,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 7,
                    RecipeId = 2,
                    Name = "Meat",
                    Amount = 2,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });

            modelbuilder.Entity<Recipe>().HasData(new Recipe
            {
                Id = 3,
                Name = "Sandwich",
                Portions = 1,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                IsDeleted = false
            });

            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 8,
                    RecipeId = 3,
                    Name = "Dough",
                    Amount = 1,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 9,
                    RecipeId = 3,
                    Name = "Cucumber",
                    Amount = 1,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });

            modelbuilder.Entity<Recipe>().HasData(new Recipe
            {
                Id = 4,
                Name = "Pasta",
                Portions = 2,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                IsDeleted = false
            });

            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 10,
                    RecipeId = 4,
                    Name = "Dough",
                    Amount = 2,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 11,
                    RecipeId = 4,
                    Name = "Tomato",
                    Amount = 1,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 12,
                    RecipeId = 4,
                    Name = "Cheese",
                    Amount = 2,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 13,
                    RecipeId = 4,
                    Name = "Meat",
                    Amount = 1,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });

            modelbuilder.Entity<Recipe>().HasData(new Recipe
            {
                Id = 5,
                Name = "Salad",
                Portions = 3,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                IsDeleted = false
            });

            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 14,
                    RecipeId = 5,
                    Name = "Lettuce",
                    Amount = 2,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 15,
                    RecipeId = 5,
                    Name = "Tomato",
                    Amount = 2,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });

            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 16,
                    RecipeId = 5,
                    Name = "Cucumber",
                    Amount = 1,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 17,
                    RecipeId = 5,
                    Name = "Cheese",
                    Amount = 2,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 18,
                    RecipeId = 5,
                    Name = "Olives",
                    Amount = 1,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });

            modelbuilder.Entity<Recipe>().HasData(new Recipe
            {
                //Friday treat
                Id = 6,
                Name = "Pizza",
                Portions = 4,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                IsDeleted = false
            });

            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 19,
                    RecipeId = 6,
                    Name = "Dough",
                    Amount = 3,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 20,
                    RecipeId = 6,
                    Name = "Tomato",
                    Amount = 2,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 21,
                    RecipeId = 6,
                    Name = "Cheese",
                    Amount = 3,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });
            modelbuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    Id = 22,
                    RecipeId = 6,
                    Name = "Olives",
                    Amount = 1,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    IsDeleted = false
                });

            modelbuilder.Entity<IngredientAvailability>().HasData(new IngredientAvailability
            {
                Id = 1,
                Name = "Cucumber",
                Amount = 2
            });
            modelbuilder.Entity<IngredientAvailability>().HasData(new IngredientAvailability
            {
                Id = 2,
                Name = "Olives",
                Amount = 2
            });
            modelbuilder.Entity<IngredientAvailability>().HasData(new IngredientAvailability
            {
                Id = 3,
                Name = "Lettuce",
                Amount = 3
            });
            modelbuilder.Entity<IngredientAvailability>().HasData(new IngredientAvailability
            {
                Id = 4,
                Name = "Meat",
                Amount = 6
            });
            modelbuilder.Entity<IngredientAvailability>().HasData(new IngredientAvailability
            {
                Id = 5,
                Name = "Tomato",
                Amount = 6
            });
            modelbuilder.Entity<IngredientAvailability>().HasData(new IngredientAvailability
            {
                Id = 6,
                Name = "Cheese",
                Amount = 8
            });
            modelbuilder.Entity<IngredientAvailability>().HasData(new IngredientAvailability
            {
                Id = 7,
                Name = "Dough",
                Amount = 10
            });
        }

    }
}
