using AniWatch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace AniWatch.Data.Enums
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
       

            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            
            }



            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Character_Anime>().HasKey(am => new
                {
                    am.CharacterId,
                    am.AnimeId
                });

                modelBuilder.Entity<Character_Anime>().HasOne(m => m.Anime).WithMany(am => am.Anime_Characters).HasForeignKey(m => m.AnimeId);
                modelBuilder.Entity<Character_Anime>().HasOne(m => m.Character).WithMany(am => am.Anime_Characters).HasForeignKey(m => m.CharacterId);


                base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            { Name = "Admin", NormalizedName = "ADMIN", Id = "admin-role" });

           

        }

        public DbSet<Character> Characters { get; set; }
            public DbSet<Anime> Anime { get; set; }
            public DbSet<Character_Anime> Actors_Movies { get; set; }
            public DbSet<Cinema> Cinemas { get; set; }
            public DbSet<Producer> Producers { get; set; }
        public DbSet<CartItem> cartItems { get; set; }


    }
}