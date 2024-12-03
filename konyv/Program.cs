using konyv;
using System;
using System.Collections.Generic;
using System.Linq;

namespace konyv
{
    namespace BookStore
    {
        internal class Program
        {
            private static void Main(string[] args)
            {
                var random = new Random();
                var books = new List<Book>();

                var hungarianTitles = new[] { "Cica", "Kutya", "Madár", "Mókus" };
                var englishTitles = new[] { "Blue", "Night", "Sunshine", "Moon" };
                var names = new[] { "Nagy László", "Horváth János", "Joe Smith", "Kovács Péter", "Peter Griffin" };

                for (int i = 0; i < 15; i++)
                {
                    string language = random.NextDouble() < 0.8 ? "Magyar" : "Angol";
                    string title = language == "Magyar"
                        ? hungarianTitles[random.Next(hungarianTitles.Length)]
                        : englishTitles[random.Next(englishTitles.Length)];

                    string isbn;
                    do
                    {
                        isbn = GenerateRandomISBN();
                    } while (books.Any(b => b.ISBN == isbn));

                    int authorCount = random.Next(1, 4); 
                    var selectedAuthors = Enumerable.Range(0, authorCount)
                                                    .Select(_ => names[random.Next(names.Length)])
                                                    .ToArray();

                    int stock = random.NextDouble() < 0.3 ? 0 : random.Next(5, 11); 
                    int price = random.Next(10, 101) * 100; 

                    books.Add(new Book(isbn, title, random.Next(2007, DateTime.Now.Year + 1), language, price, stock, selectedAuthors));
                }

                int totalRevenue = 0, outOfStockCount = 0;
                int initialStock = books.Sum(b => b.Stock);

                for (int i = 0; i < 100; i++)
                {
                    if (!books.Any()) break;

                    var book = books[random.Next(books.Count)];

                    if (book.Stock > 0)
                    {
                        book.Stock--;
                        totalRevenue += book.Price;
                    }
                    else if (random.NextDouble() < 0.5)
                    {
                        book.Stock += random.Next(1, 11);
                    }
                    else
                    {
                        books.Remove(book);
                        outOfStockCount++;
                    }
                }

                int finalStock = books.Sum(b => b.Stock);
                Console.WriteLine($"Teljes bevétel: {totalRevenue} Ft");
                Console.WriteLine($"Elfogyott könyvek száma: {outOfStockCount}");
                Console.WriteLine($"Változások: Kezdeti: {initialStock}, Jelenlegi: {finalStock}, Különbözet: {finalStock - initialStock}");
            }

            private static string GenerateRandomISBN()
            {
                var random = new Random();
                return string.Concat(Enumerable.Range(0, 10).Select(_ => random.Next(0, 10).ToString()));
            }
        }
    }
}