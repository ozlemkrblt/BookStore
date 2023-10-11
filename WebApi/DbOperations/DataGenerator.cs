using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.DbOperations;
using WebApi.Common;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Genres.AddRange(
                    new Genre
                    {
                        Name="Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Novel"
                    }

                );

                context.Books.AddRange
                 (
                     new Book
                     {
                         
                         Title = "Lean Startup",
                         GenreId = 1, //Id 1 = Personal growth
                         TotalPages = 200,
                         PublishDate = new DateTime(2001, 06, 12)
                     },
                    new Book
                    {
                        
                        Title = "Herland",
                        GenreId = 2, //Id 2=Science Fiction
                        TotalPages = 250,
                        PublishDate = new DateTime(2010, 05, 23)
                    },
                    new Book
                    {
                        
                        Title = "Dune",
                        GenreId = 2, //Id 2=Science Fiction
                        TotalPages = 540,
                        PublishDate = new DateTime(1957, 07, 05)
                    }

                );

                context.SaveChanges(); 
            }
        }
    }
}