using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaZaKnigi.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProdavnicaZaKnigiContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ProdavnicaZaKnigiContext>>()))
            {
                if (context.Book.Any() || context.Author.Any() || context.Genre.Any() || context.Review.Any())
                {
                    return;
                }

                context.Author.AddRange(
                    new Author { FirstName = "Robert", LastName = "Greene", BirthDate = DateTime.Parse("1959-05-14"), Nationality = "American", Gender = "Male" },
                    new Author { FirstName = "Louisa May", LastName = "Alcott", BirthDate = DateTime.Parse("1832-11-29"), Nationality = "American", Gender = "Female" },
                    new Author { FirstName = "Jane", LastName = "Austen", BirthDate = DateTime.Parse("1775-12-16"), Nationality = "English", Gender = "Female" },
                    new Author { FirstName = "Charles", LastName = "Bukowski", BirthDate = DateTime.Parse("1920-08-16"), Nationality = "German-American", Gender = "Male" },
                   new Author { FirstName = "Franz", LastName = "Kafka", BirthDate = DateTime.Parse("1883-07-03"), Nationality = "Bohemian Jewish", Gender = "Male" }

                     );
                context.SaveChanges();

                context.Genre.AddRange(
                   new Genre { GenreName = "Romance" }, //1
                    new Genre { GenreName = "Mystery" }, //2
                    new Genre { GenreName = "Thriller" }, //3
                    new Genre { GenreName = "Biography" }, //4
                    new Genre { GenreName = "Fantasy" }, //5
                    new Genre { GenreName = "Psychological" }, //6
                    new Genre { GenreName = "Satire" }, //7
                    new Genre { GenreName = "Fiction" }, //8
                    new Genre { GenreName = "Non-fiction" }, //9
                    new Genre { GenreName = "Comedy" } //10
                );
                context.SaveChanges();

                context.Book.AddRange(
                    new Book
                    {
                        Title = "The 48 laws of Power",
                        AuthorId = context.Author.Single(a => a.FirstName == "Robert" && a.LastName == "Greene").Id,
                        YearPublished = 1998,
                        Publisher= "Viking Press",
                        NumPages = 480,
                        Description = "The 48 Laws of Power (1998) is a self-help book by " +
                        "American author Robert Greene.The book is a New York Times bestseller, " +
                        "selling over 1.2 million copies in the United States.",
                        DownloadUrl = "https://www.ciuohecampus.com/images/concise48.pdf",
                        FrontPage = "https://upload.wikimedia.org/wikipedia/en/9/9d/GreeneRobert-48LawsOfPower.jpg"

                    },
                    new Book
                    {
                        Title = "Little Women",
                        AuthorId = context.Author.Single(a => a.FirstName == "Louisa May" && a.LastName == "Alcott").Id,
                        YearPublished = 1868,
                        NumPages = 759,
                        Description = "Little Women is a coming-of-age novel written by American novelist Louisa May Alcott," +
                        " originally published in two volumes in 1868 and 1869. " +
                        "The story follows the lives of the four March sisters—Meg, Jo, Beth, and Amy—and details their passage" +
                        " from childhood to womanhood. Loosely based on the lives of the author and her three sisters," +
                        " it is classified as an autobiographical or semi-autobiographical novel",
                        Publisher = "Roberts Brothers",
                        DownloadUrl = "https://www.defence.lk/upload/ebooks/Little%20Women.pdf",
                        FrontPage = "https://upload.wikimedia.org/wikipedia/commons/f/f9/Houghton_AC85.A%E2%84%93194L.1869_pt.2aa_-_Little_Women%2C_title.jpg",
                   
                    },
                    new Book
                    {
                        Title = "Pride and Prejudice",
                        AuthorId = context.Author.Single(a => a.FirstName == "Jane" && a.LastName == "Austen").Id,
                        YearPublished = 1813,
                        Publisher = "T. Egerton, Whitehall",
                        NumPages = 448,
                        Description = "Pride and Prejudice is the second novel by English author Jane Austen, published in 1813. " +
                        "A novel of manners, it follows the character development of Elizabeth Bennet," +
                        " the protagonist of the book, who learns about the repercussions of hasty " +
                        "judgments and comes to appreciate the difference between superficial goodness and actual goodness.",
                        
                        DownloadUrl = "https://www.gutenberg.org/files/1342/old/pandp12p.pdf",
                        FrontPage = "https://upload.wikimedia.org/wikipedia/commons/1/17/PrideAndPrejudiceTitlePage.jpg"
                    },
                     new Book
                     {
                         Title = "Ham on Rye",
                         AuthorId = context.Author.Single(a => a.FirstName == "Charles" && a.LastName == "Bukowski").Id,
                         YearPublished = 1982,
                         Publisher = "Black Sparrow Books",
                         NumPages = 288,
                         Description = "Ham on Rye is a 1982 semi-autobiographical novel by American author and poet Charles Bukowski." +
                         " Written in the first person, the novel follows Henry Chinaski, Bukowski's thinly veiled alter ego, during his early years." +
                         " Written in Bukowski's characteristically straightforward prose, " +
                         "the novel tells of his coming-of-age in Los Angeles during the Great Depression.",
                         
                         DownloadUrl = "https://englishonlineclub.com/pdf/Charles%20Bukowski%20-%20Ham%20on%20Rye%20%5BEnglishOnlineClub.com%5D.pdf",
                         FrontPage = "https://upload.wikimedia.org/wikipedia/en/2/20/HamOnRye.jpg"
                     },
                      new Book
                      {
                          Title = "The Metamorphosis",
                          AuthorId = context.Author.Single(a => a.FirstName == "Franz" && a.LastName == "Kafka").Id,
                          YearPublished = 1915,
                          Publisher="Kurt Wolff Verlag",
                          NumPages = 70,
                          Description = "The Metamorphosis (German: Die Verwandlung) is a novella written by Franz Kafka and first published in 1915. " +
                          "One of Kafka's best-known works, Metamorphosis tells the story of salesman Gregor Samsa," +
                          " who wakes one morning to find himself inexplicably transformed into a huge insect and " +
                          "subsequently struggles to adjust to this new condition. ",
                         
                          DownloadUrl = "https://psychology.okstate.edu/faculty/jgrice/psyc4333/Franz_Kafka_The_Metamorphosis.pdf",
                          FrontPage = "https://upload.wikimedia.org/wikipedia/commons/a/a5/Franz_Kafka_Die_Verwandlung_1916_Orig.-Pappband.jpg"
                      }
                    );
                context.SaveChanges();

                context.BookGenre.AddRange(
                    new BookGenre { GenreId = 6, BookId = 1 },
                    
                    new BookGenre { GenreId = 1, BookId = 2 },
                    new BookGenre { GenreId = 10, BookId = 2 },

                    new BookGenre { GenreId = 1, BookId = 3 },
                    new BookGenre { GenreId = 7, BookId = 3 },
                    new BookGenre { GenreId = 8, BookId = 3 },
                    new BookGenre { GenreId = 8, BookId = 4 },
                    new BookGenre { GenreId = 4, BookId = 4 },
                    new BookGenre { GenreId = 6, BookId = 5 },
                    new BookGenre { GenreId = 5, BookId = 5 }
                );

                context.SaveChanges();

            }
        }
    }
}