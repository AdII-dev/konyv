using System;
using System.Collections.Generic;
using System.Linq;

namespace konyv
{
    internal class Book
    {
        public string ISBN { get; private set; }
        public string Title { get; private set; }
        public int PublicationYear { get; private set; }
        public string Language { get; private set; }
        public int Price { get; private set; }
        public int Stock { get; set; }
        public List<Author> Authors { get; private set; }

        public Book(string isbn, string title, int publicationYear, string language, int price, int stock, params string[] authorNames)
        {
            if (isbn.Length != 10 || !isbn.All(char.IsDigit))
                throw new ArgumentException("Az ISBN-nek pontosan 10 számjegyből kell állnia.");
            if (string.IsNullOrWhiteSpace(title) || title.Length < 3 || title.Length > 64)
                throw new ArgumentException("A címnek 3 és 64 karakter közötti hosszúságúnak kell lennie.");
            if (publicationYear < 2007 || publicationYear > DateTime.Now.Year)
                throw new ArgumentException("A kiadás évének 2007 és a jelenlegi év közé kell esnie.");
            if (!new[] { "Magyar", "Angol", "Német" }.Contains(language))
                throw new ArgumentException("Csak Magyar, Angol, vagy Német nyelv választható.");
            if (price < 1000 || price > 10000 || price % 100 != 0)
                throw new ArgumentException("Az árnak 1000 és 10000 közé kell esnie, kerek 100-as értékben.");
            if (authorNames.Length < 1 || authorNames.Length > 3)
                throw new ArgumentException("A könyvnek 1 és 3 szerzője között kell lennie.");

            ISBN = isbn;
            Title = title;
            PublicationYear = publicationYear;
            Language = language;
            Price = price;
            Stock = stock;

            Authors = authorNames.Select(name => new Author(name)).ToList();
        }

        public Book(string title, string authorName)
        {
            ISBN = GenerateRandomISBN();
            Title = title;
            PublicationYear = 2024;
            Language = "Magyar";
            Price = 4500;
            Stock = 0;
            Authors = new List<Author> { new Author(authorName) };
        }

        private static string GenerateRandomISBN()
        {
            var random = new Random();
            return string.Concat(Enumerable.Range(0, 10).Select(_ => random.Next(0, 10).ToString()));
        }

        public override string ToString()
        {
            var authorsString = Authors.Count == 1 ? "Szerző:" : "Szerzők:";
            var stockString = Stock > 0 ? $"{Stock} db" : "Beszerzés alatt";
            return $"{authorsString} {string.Join(", ", Authors)}\n" +
                   $"Cím: {Title}\n" +
                   $"Kiadás éve: {PublicationYear}\n" +
                   $"Nyelv: {Language}\n" +
                   $"Ár: {Price} Ft\n" +
                   $"Készlet: {stockString}";
        }
    }
}