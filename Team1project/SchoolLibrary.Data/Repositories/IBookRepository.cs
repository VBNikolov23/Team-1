namespace SchoolLibrary.Data.Repositories;
using SchoolLibrary.Data.Models;
using System.Collections.Generic;

public interface IBookRepository
{
    IReadOnlyList<Book> GetAll();
    Book? GetById(int id);
    void Add(Book book);
}