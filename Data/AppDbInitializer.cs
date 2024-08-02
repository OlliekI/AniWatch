using AniWatch.Data.Enums;
using AniWatch.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AniWatch.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Cinema
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name = "Cinema 1",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 2",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 3",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-3.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 4",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-4.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 5",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-5.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                    });
                    context.SaveChanges();
                }
                //Characters
                if (!context.Characters.Any())
                {
                    context.Characters.AddRange(new List<Character>()
                    {
                        new Character()
                        {
                            FullName = "Luffy",
                            Bio = "Luffy D. Monkey",
                            ProfilePictureURL = "https://preview.redd.it/tgnwy4m2ju741.jpg?auto=webp&s=7161918bd0eef1f09da4ad44ddff7812307125a8"

                        },
                        new Character()
                        {
                            FullName = "Ichigo",
                            Bio = "Ichigo Kurasaki",
                            ProfilePictureURL = "https://wallpapers.com/images/hd/bleach-pictures-4t2ieybhrb5agrn6.jpg"
                        },
                        new Character()
                        {
                            FullName = "Sasuki",
                            Bio = "Sasuki Uchiha",
                            ProfilePictureURL = "https://imagedelivery.net/LBWXYQ-XnKSYxbZ-NuYGqQ/f0dd9964-4684-4569-fc2b-1caf42c98600/avatarhd"
                        },
                        new Character()
                        {
                            FullName = "Naruto",
                            Bio = "Naruto Uzumaki",
                            ProfilePictureURL = "https://img.wattpad.com/be7913fdc9eba8c7367fcab45a29a4a635538f64/68747470733a2f2f73332e616d617a6f6e6177732e636f6d2f776174747061642d6d656469612d736572766963652f53746f7279496d6167652f47753556436c77796956356745773d3d2d3230342e313637336235376132336130323263333434303536393032363537342e6a7067"
                        },
                        new Character()
                        {
                            FullName = "Gon",
                            Bio = "Gon Freecss",
                            ProfilePictureURL = "https://i.pinimg.com/originals/d1/94/02/d194025f6221caef42fd905c26f8705a.jpg"
                        }
                    });
                    context.SaveChanges();
                }
                //Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName = "Studio Pierrot",
                            Bio = "Pierrot Inc.",
                            ProfilePictureURL = "https://static.wikia.nocookie.net/mitchell-van-morgan/images/1/14/Studio_Pierrot_logo.png/revision/latest?cb=20181011000622"

                        },
                        new Producer()
                        {
                            FullName = "Toei Animation",
                            Bio = "Toei Animation",
                            ProfilePictureURL = "https://corp.toei-anim.co.jp/en/index/mv-movie/main/00/teaserItems2/0/binaryNodeName/SP_TOP_03.png"
                        },
                        new Producer()
                        {
                            FullName = "Studio Ghibli",
                            Bio = "Studio Ghibli",
                            ProfilePictureURL = "https://christandpopculture.com/wp-content/uploads/2014/08/20140816.ghibliA.jpg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 4",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-4.jpeg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 5",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-5.jpeg"
                        }
                    });
                    context.SaveChanges();
                }
                //Anime
                if (!context.Anime.Any())
                {
                    context.Anime.AddRange(new List<Anime>()
                    {
                        new Anime()
                        {
                            Name = "One Piece",
                            Description = "Will Luffy find the one piece?",
                            Price = 45.5,
                            ImageURL = "https://m.media-amazon.com/images/I/919JU1ypbOS._AC_SL1500_.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            CinemaId = 3,
                            ProducerId = 2,
                            MovieCategory = AnimeCategory.Adventure
                        },
                        new Anime()
                        {
                            Name = "Bleach",
                            Description = "Follow Ichigo to uncover the truth",
                            Price = 35.5,
                            ImageURL = "https://m.media-amazon.com/images/I/61tnqxXlGEL._AC_UF1000,1000_QL80_.jpg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3),
                            CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = AnimeCategory.Action
                        },
                        new Anime()
                        {
                            Name = "Naruto",
                            Description = "Naruto is trying to become the hokage will he succeed?",
                            Price = 25.5,
                            ImageURL = "https://media.takealot.com/covers_images/24ee094158d54b949caa9f7466294f8c/s-pdpxl.file",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            CinemaId = 4,
                            ProducerId = 1,
                            MovieCategory = AnimeCategory.Action
                        },
                        new Anime()
                        {
                            Name = "Bleach TYBW",
                            Description = "Second season of Bleach",
                            Price = 55.5,
                            ImageURL = "https://m.media-amazon.com/images/M/MV5BZThlOTJiMTUtMjcyNC00YjhkLTk4ZDItOWQxZTI0YjZlMWQzXkEyXkFqcGdeQXVyMTU2Mjg2NjE2._V1_.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-5),
                            CinemaId = 1,
                            ProducerId = 2,
                            MovieCategory = AnimeCategory.Adventure
                        },
                        new Anime()
                        {
                            Name = "Naruto Shippuden",
                            Description = "second season of Naruto",
                            Price = 39.50,
                            ImageURL = "https://m.media-amazon.com/images/I/81Zj-BWityL._AC_SL1500_.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = AnimeCategory.Action
                        },
                        new Anime()
                        {
                            Name = "Hunter x Hunter",
                            Description = "Gon is trying to find his father",
                            Price = 29.5,
                            ImageURL = "https://cdn.europosters.eu/image/1300/posters/hunter-x-hunter-heroes-i122814.jpg",
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            CinemaId = 1,
                            ProducerId = 5,
                            MovieCategory = AnimeCategory.Adventure
                        }
                    });
                    context.SaveChanges();
                }
                //Characters & Anime
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Character_Anime>()
                    {
                        new Character_Anime()
                        {
                            CharacterId = 1,
                            AnimeId = 1
                        },
                        new Character_Anime()
                        {
                            CharacterId = 3,
                            AnimeId = 1
                        },

                         new Character_Anime()
                        {
                            CharacterId = 1,
                            AnimeId = 2
                        },
                         new Character_Anime()
                        {
                            CharacterId = 4,
                            AnimeId = 2
                        },

                        new Character_Anime()
                        {
                            CharacterId = 1,
                            AnimeId = 3
                        },
                        new Character_Anime()
                        {
                            CharacterId = 2,
                            AnimeId = 3
                        },
                        new Character_Anime()
                        {
                            CharacterId = 5,
                            AnimeId = 3
                        },


                        new Character_Anime()
                        {
                            CharacterId = 2,
                            AnimeId = 4
                        },
                        new Character_Anime()
                        {
                            CharacterId = 3,
                            AnimeId = 4
                        },
                        new Character_Anime()
                        {
                            CharacterId = 4,
                            AnimeId = 4
                        },


                        new Character_Anime()
                        {
                            CharacterId = 2,
                            AnimeId = 5
                        },
                        new Character_Anime()
                        {
                            CharacterId = 3,
                            AnimeId = 5
                        },
                        new Character_Anime()
                        {
                            CharacterId = 4,
                            AnimeId = 5
                        },
                        new Character_Anime()
                        {
                            CharacterId = 5,
                            AnimeId = 5
                        },


                        new Character_Anime()
                        {
                            CharacterId = 3,
                            AnimeId = 6
                        },
                        new Character_Anime()
                        {
                            CharacterId = 4,
                            AnimeId = 6
                        },
                        new Character_Anime()
                        {
                            CharacterId = 5,
                            AnimeId = 6
                        },
                    });
                    context.SaveChanges();
                }
            }

        }
    }
}