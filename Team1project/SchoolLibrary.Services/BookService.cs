namespace SchoolLibrary.Services;
using SchoolLibrary.Data.Repositories;
using SchoolLibrary.Data.Models;
public class BookService
{
    private readonly IBookRepository repository;

    public BookService(IBookRepository repository)
    {
        this.repository = repository;
    }

    public IReadOnlyList<Book> GetAllBooks()
    {
        return repository.GetAll();
    }

    // Задача 2
    public Book? GetBook(int id)
    {
        return repository.GetById(id);
    }

    // Задача 4
    public IReadOnlyList<Book> SearchBooks(string? query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return repository.GetAll();
        }

        string text = query.Trim();
        var result = new List<Book>();

        foreach (Book book in repository.GetAll())
        {
            if (book.Title.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                book.Author.Contains(text, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(book);
            }
        }

        return result;
    }

    // Задача 3
    public string AddBook(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
        {
            return "Моля, попълнете заглавие и автор.";
        }

        repository.Add(new Book { Title = title.Trim(), Author = author.Trim() });
        return "Книгата е добавена успешно.";
    }

    public string BorrowBook(int id)
    {
        Book? book = repository.GetById(id);

        if (book == null)
        {
            return "Книгата не е намерена.";
        }

        if (book.IsBorrowed)
        {
            return "Книгата вече е заета.";
        }
        book.IsBorrowed = true;
        return "Книгата е заета успешно.";
    }

    public string ReturnBook(int id)
    {
        Book? book = repository.GetById(id);

        if (book == null)
        {
            return "Книгата не е намерена.";
        }

        if (!book.IsBorrowed)
        {
            return "Книгата не е заета.";
        }
        book.IsBorrowed = false;
        return "Книгата е върната успешно.";
    }
}