using System;

namespace konyv
{
    public class Author
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public Author(string fullName)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[A-Za-záéíóöőúüűÁÉÍÓÖŐÚÜŰ]{3,32} [A-Za-záéíóöőúüűÁÉÍÓÖŐÚÜŰ]{3,32}$"))
                throw new ArgumentException("A névnek pontosan két szóból kell állnia (keresztnév és vezetéknév), mindkettőnek 3 és 32 karakter között kell lennie.");

            var parts = fullName.Split(' ');
            FirstName = parts[0];
            LastName = parts[1];
            Id = Guid.NewGuid();
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}