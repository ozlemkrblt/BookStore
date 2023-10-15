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

                context.Authors.AddRange(
                   new Author
                   {
                       Name = "Eric" ,
                       Surname= "Ries",
                       Bio = "Eric Ries is an American entrepreneur, blogger, and author of The Lean Startup, a book on the lean startup movement.",
                       BirthDate = new DateTime(1978, 09, 22)
                   },
                   new Author
                   {
                       Name = "Charlotte",
                       Surname = "Perkins Gilman",
                       Bio = "Charlotte Perkins Gilman, in full Charlotte Anna Perkins Stetson Gilman,American feminist, lecturer, writer, and publisher who was a leading theorist of the women’s movement in the United States.",
                       BirthDate = new DateTime(1860, 07, 03)
                   },
                   new Author
                   {
                       Name = "Frank",
                       Surname = "Herbert",
                       Bio = "Frank Herbert was an American science fiction author best known for the 1965 novel Dune and its five sequels.",
                       BirthDate = new DateTime(1920, 10, 08)
                   }

               );


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
                         PublishDate = new DateTime(2001, 06, 12),
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

                ); ;; ; ; ;

                context.SaveChanges();
            }
        }
    }
}