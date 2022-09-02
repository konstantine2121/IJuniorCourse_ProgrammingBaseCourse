using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Создать хранилище книг.<br/>
    ///Каждая книга имеет название, автора и год выпуска (можно добавить еще параметры). <br/>
    ///В хранилище можно добавить книгу, убрать книгу, показать все книги и показать книги по указанному параметру <br/>
    ///(по названию, по автору, по году выпуска).
    /// </summary>
    class BookStorageTask : IRunnable
    {
        #region IRunnable Implementation
        
        public void Run()
        {
            int LastCenturyBegin = 1900;
            int LastCenturyEnd = 1999;

            var storage = new BookStorage();

            storage.Add(new Book("Маленький принц", "Антуан де Сент-Экзюпери", 1943));
            storage.Add(new Book("Гарри Поттер", "Джоан Роулинг", 1995));
            storage.Add(new Book("Алиса в Стране чудес", "Льюис Кэрролл", 1865));
            storage.Add(new Book("Алиса в Зазеркалье", "Льюис Кэрролл", 1871));

            PrintBookStorageInfo(storage);

            var foundLuisBooks = storage.FindByAuthor("Льюис Кэрролл");

            Console.WriteLine("Книги 'Льюис Кэрролла': ");
            PrintBooksInfo(foundLuisBooks);

            var foundLastCenturyBooks = storage.FindByYearOfRelease(LastCenturyBegin,LastCenturyEnd);

            Console.WriteLine("Книги 19го века: ");
            PrintBooksInfo(foundLastCenturyBooks);

            var foundGarryPotterBooks= storage.FindByName("Гарри Поттер");

            Console.WriteLine("Книги про Гарри Поттера: ");
            PrintBooksInfo(foundGarryPotterBooks);


            Console.WriteLine("Убираем книгу про Гарри Поттера.\n");
            storage.Remove(foundGarryPotterBooks.First());

            PrintBookStorageInfo(storage);

            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private void PrintBookStorageInfo(BookStorage storage)
        {
            Console.WriteLine("Хранилище книг: ");

            var books = storage.GetAllBooks();

            PrintBooksInfo(books);

            Console.WriteLine();
        }

        private void PrintBooksInfo(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                PrintBookInfo(book);
            }
            Console.WriteLine();
        }

        private void PrintBookInfo(Book book)
        {
            const string format = "{0,-30}\t{1,-30}\t{2,5}";
            Console.WriteLine(format, book.Name,book.Author,book.YearOfRelease);
        }

        #region Private Classes

        private class Book
        {
            public Book(string name, string author, int yearOfRelease)
            {
                Name = name;
                Author = author;
                YearOfRelease = yearOfRelease;
            }

            public string Name { get; private set; }

            public string Author { get; private set; }
            
            public int YearOfRelease{ get; private set; }
        }

        private class BookStorage
        {
            private readonly List<Book> _books = new List<Book>();

            public void Add(Book book)
            {
                if (book == null)
                {
                    throw new ArgumentNullException(nameof(book));
                }

                _books.Add(book);
            }

            public void Remove(Book book)
            {
                if (book == null)
                {
                    throw new ArgumentNullException(nameof(book));
                }

                var bookToDelete = _books.FirstOrDefault(
                    element =>
                    element.Name.Equals(book.Name) 
                    && element.Author.Equals(book.Author)
                    && element.YearOfRelease.Equals(book.YearOfRelease));

                if (bookToDelete != null)
                {
                    _books.Remove(bookToDelete);
                }
            }

            public IReadOnlyCollection<Book> GetAllBooks()
            {
                return _books;
            }

            public IEnumerable<Book> FindByName(string name)
            {
                return _books.Where(element => element.Name.Equals(name));
            }

            public IEnumerable<Book> FindByAuthor(string author)
            {
                return _books.Where(element => element.Author.Equals(author));
            }

            public IEnumerable<Book> FindByYearOfRelease(int  year)
            {
                return _books.Where(element => element.YearOfRelease.Equals(year));
            }

            public IEnumerable<Book> FindByYearOfRelease(int intervalBegin, int intervalEnd)
            {
                return _books.Where(element => intervalBegin <= element.YearOfRelease && element.YearOfRelease <= intervalEnd);
            }
        }

        #endregion Private Classes

    }
}
