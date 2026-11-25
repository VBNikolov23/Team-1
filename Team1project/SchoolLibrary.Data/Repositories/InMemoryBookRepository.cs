namespace SchoolLibrary.Data.Repositories;
using SchoolLibrary.Data.Models;
using System.Collections.Generic;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = new List<Book>
    {
        new Book { Id = 1, Title = "Под игото", Author = "Иван Вазов" },
        new Book { Id = 2, Title = "Малкият принц", Author = "Антоан дьо Сент Екзюпери" },
        new Book { Id = 3, Title = "Пипи Дългото чорапче", Author = "Астрид Линдгрен" }
    };

    public IReadOnlyList<Book> GetAll()
    {
        return _books.AsReadOnly();
    }

    public Book? GetById(int id)
    {
        foreach (Book book in _books)
        {
            if (book.Id == id)
            {
                return book;
            }
        }
        return null;
    }

    public void Add(Book book)
    {
        int maxId = 0;
        foreach (Book existing in _books)
        {
            if (existing.Id > maxId)
            {
                maxId = existing.Id;
            }
        }

        book.Id = maxId + 1;
        _books.Add(book);
    }
}