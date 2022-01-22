using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using Business.Entities;

namespace Repository.Data
{
    public class Context : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Reader> Readers { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //init entities
            Author[] authors = new Author[]
                {
                    new Author { Id = 1, Name = "Ф. М. Достоевский" },
                    new Author { Id = 2, Name = "Н. В. Гоголь" },
                    new Author { Id = 3, Name = "Стивен Кинг" },
                };
            Genre[] genres = new Genre[]
                {
                    new Genre { Id = 1, Name = "Роман" },
                    new Genre { Id = 2, Name = "Триллер" },
                    new Genre { Id = 3, Name = "Драма" },
                    new Genre { Id = 4, Name = "Комедия" },
                    new Genre { Id = 5, Name = "Пьеса" },
                    new Genre { Id = 6, Name = "Приключение" },
                    new Genre { Id = 7, Name = "Рассказ" },
                };
            Book[] books = new Book[]
                {
                    new Book { Id = 1, Name = "Преступление и наказание", Pages = 247, ReaderId = 5, LibraryId = 1 },
                    new Book { Id = 2, Name = "Дурак", Pages = 127, ReaderId = 1, LibraryId = 1 },
                    new Book { Id = 3, Name = "Дурак", Pages = 127, ReaderId = 2, LibraryId = 2 },
                    new Book { Id = 4, Name = "Оно", Pages = 780, ReaderId = 1, LibraryId = 1 },
                    new Book { Id = 5, Name = "Нужные вещи", Pages = 452, ReaderId = 3, LibraryId = 2 },
                    new Book { Id = 6, Name = "Нужные вещи", Pages = 452, ReaderId = 4, LibraryId = 3 },
                    new Book { Id = 7, Name = "Нос", Pages = 138, ReaderId = 5, LibraryId = 3 },
                };
            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Genre>().HasData(genres);
            #region init many-to-many
            books[0].Authors.Add(authors[0]);
            books[1].Authors.Add(authors[0]);
            books[2].Authors.Add(authors[0]);
            books[3].Authors.Add(authors[2]);
            books[4].Authors.Add(authors[2]);
            books[5].Authors.Add(authors[2]);
            books[6].Authors.Add(authors[1]);

            books[0].Genres.Add(genres[0]);
            books[1].Genres.Add(genres[0]);
            books[2].Genres.Add(genres[0]);
            books[3].Genres.Add(genres[0]);
            books[3].Genres.Add(genres[1]);
            books[4].Genres.Add(genres[0]);
            books[4].Genres.Add(genres[1]);
            books[5].Genres.Add(genres[0]);
            books[5].Genres.Add(genres[1]);
            books[6].Genres.Add(genres[6]);
            books[6].Genres.Add(genres[3]);
            #endregion
            modelBuilder.Entity<Book>().HasData(books);

            modelBuilder.Entity<Library>().HasData(
                new Library[]
                {
                    new Library { Id = 1, Address = "ул. Пушкина, д. 24" },
                    new Library { Id = 2, Address = "ул. Грушевая, д. 2" },
                    new Library { Id = 3, Address = "ул. Брусилова, д.15" },
                });
            modelBuilder.Entity<Employee>().HasData(
                new Employee[]
                {
                    new Employee { Id = 1, Firstname = "Иоанн", Lastname = "Богослов", Salary = 25000, LibraryId = 1 },
                    new Employee { Id = 2, Firstname = "Петр", Lastname = "Сидоров", Salary = 25000, LibraryId = 1 },
                    new Employee { Id = 3, Firstname = "Даниил", Lastname = "Плотников", Salary = 35000, LibraryId = 1 },
                    new Employee { Id = 4, Firstname = "Сергей", Lastname = "Кудрявцев", Salary = 20000, LibraryId = 2 },
                    new Employee { Id = 5, Firstname = "Павел", Lastname = "Скороходов", Salary = 25000, LibraryId = 2 },
                    new Employee { Id = 6, Firstname = "Дмитрий", Lastname = "Борисов", Salary = 15000, LibraryId = 3 },
                    new Employee { Id = 7, Firstname = "Федор", Lastname = "Федоров", Salary = 25000, LibraryId = 3 },
                    new Employee { Id = 8, Firstname = "Иван", Lastname = "Иванов", Salary = 30000, LibraryId = 3 },
                });
            modelBuilder.Entity<Reader>().HasData(
                new Reader[]
                {
                    new Reader { Id = 1, Firstname = "Федор", Lastname = "Стрельников" },
                    new Reader { Id = 2, Firstname = "Кирилл", Lastname = "Аникин" },
                    new Reader { Id = 3, Firstname = "Никита", Lastname = "Кириллов" },
                    new Reader { Id = 4, Firstname = "Павел", Lastname = "Бушманов" },
                    new Reader { Id = 5, Firstname = "Петр", Lastname = "Петров" },
                });
        }   
    }
}
