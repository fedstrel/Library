using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;

namespace Repository.Data
{
    class DBInitializer
    {
        public void Initialize(Context context)
        {
            Author[] authors = new Author[]
                {
                    new Author { Name = "Ф. М. Достоевский" },
                    new Author { Name = "Н. В. Гоголь" },
                    new Author { Name = "Стивен Кинг" },
                };
            context.Authors.AddRange(authors);
            context.SaveChanges();

            Genre[] genres = new Genre[]
                {
                    new Genre { Name = "Роман" },
                    new Genre { Name = "Триллер" },
                    new Genre { Name = "Драма" },
                    new Genre { Name = "Комедия" },
                    new Genre { Name = "Пьеса" },
                    new Genre { Name = "Приключение" },
                    new Genre { Name = "Рассказ" },
                };
            context.Genres.AddRange(genres);
            context.SaveChanges();

            Library[] libraries = new Library[]
                {
                    new Library { Address = "ул. Пушкина, д. 24" },
                    new Library { Address = "ул. Грушевая, д. 2" },
                    new Library { Address = "ул. Брусилова, д.15" },
                };
            context.Libraries.AddRange(libraries);
            context.SaveChanges();

            Employee[] employees = new Employee[]
                {
                    new Employee { Firstname = "Иоанн", Lastname = "Богослов", Salary = 25000, LibraryId = libraries[0].Id },
                    new Employee { Firstname = "Петр", Lastname = "Сидоров", Salary = 25000, LibraryId = libraries[0].Id },
                    new Employee { Firstname = "Даниил", Lastname = "Плотников", Salary = 35000, LibraryId = libraries[0].Id },
                    new Employee { Firstname = "Сергей", Lastname = "Кудрявцев", Salary = 20000, LibraryId = libraries[1].Id },
                    new Employee { Firstname = "Павел", Lastname = "Скороходов", Salary = 25000, LibraryId = libraries[1].Id },
                    new Employee { Firstname = "Дмитрий", Lastname = "Борисов", Salary = 15000, LibraryId = libraries[2].Id },
                    new Employee { Firstname = "Федор", Lastname = "Федоров", Salary = 25000, LibraryId = libraries[2].Id },
                    new Employee { Firstname = "Иван", Lastname = "Иванов", Salary = 30000, LibraryId = libraries[2].Id },
                };
            context.Employees.AddRange(employees);
            context.SaveChanges();

            Reader[] readers = new Reader[]
                {
                    new Reader { Firstname = "Федор", Lastname = "Стрельников" },
                    new Reader { Firstname = "Кирилл", Lastname = "Аникин" },
                    new Reader { Firstname = "Никита", Lastname = "Кириллов" },
                    new Reader { Firstname = "Павел", Lastname = "Бушманов" },
                    new Reader { Firstname = "Петр", Lastname = "Петров" },
                };
            context.Readers.AddRange(readers);
            context.SaveChanges();

            Book[] books = new Book[]
                {
                    new Book { Name = "Преступление и наказание", Pages = 247, ReaderId = readers[4].Id, LibraryId = libraries[0].Id },
                    new Book { Name = "Дурак", Pages = 127, ReaderId = readers[0].Id, LibraryId = libraries[0].Id },
                    new Book { Name = "Дурак", Pages = 127, ReaderId = readers[1].Id, LibraryId = libraries[1].Id },
                    new Book { Name = "Оно", Pages = 780, ReaderId = readers[0].Id, LibraryId = libraries[0].Id },
                    new Book { Name = "Нужные вещи", Pages = 452, ReaderId = readers[2].Id, LibraryId = libraries[1].Id },
                    new Book { Name = "Нужные вещи", Pages = 452, ReaderId = readers[3].Id, LibraryId = libraries[2].Id },
                    new Book { Name = "Нос", Pages = 138, ReaderId = readers[4].Id, LibraryId = libraries[2].Id },
                };
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
            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
