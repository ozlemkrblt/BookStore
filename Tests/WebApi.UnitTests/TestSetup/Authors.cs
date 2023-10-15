using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Authors
{
    public static void AddAuthors(this BookStoreDbContext context)
    {
        context.Authors.AddRange
        (
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
    }
}

