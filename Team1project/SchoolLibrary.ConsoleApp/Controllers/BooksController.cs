namespace SchoolLibrary.ConsoleApp.Controllers;
using SchoolLibrary.Services;
using Microsoft.AspNetCore.Mvc;

public class BooksController : Controller
{
    private readonly BookService _service;

    public BooksController(BookService service)
    {
        _service = service;
    }

    // GET /Books?search=текст → списък (с търсене)
    public IActionResult Index(string? search)
    {
        ViewData["Search"] = search;
        var books = _service.SearchBooks(search);
        return View(books);
    }

    // GET /Books/Details/2 → една книга
    public IActionResult Details(int id)
    {
        var book = _service.GetBook(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    // GET /Books/Create → показва формата
    public IActionResult Create()
    {
        return View();
    }

    // POST /Books/Create → добавя книгата
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(string title, string author)
    {
        TempData["Message"] = _service.AddBook(title, author);
        return RedirectToAction(nameof(Index));
    }

    // POST /Books/Borrow/2 → заемане
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Borrow(int id)
    {
        TempData["Message"] = _service.BorrowBook(id);
        return RedirectToAction(nameof(Index));
    }

    // POST /Books/Return/2 → връщане
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Return(int id)
    {
        TempData["Message"] = _service.ReturnBook(id);
        return RedirectToAction(nameof(Index));
    }
}