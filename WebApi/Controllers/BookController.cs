using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.AddBook;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;
using static WebApi.BookOperations.AddBook.AddBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var res = query.Handle();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            return _context.Books.First(book => book.Id == id);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] AddBookModel newBook)
        {
            try
            {
                AddBookCommand addBook = new AddBookCommand(_context);
                addBook.addBookModel = newBook;
                addBook.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(book => book.Id == id);

            if (book is null)
            {
                return BadRequest();
            }

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(book => book.Id == id);

            if (book is null)
            {
                return BadRequest();
            }

            _context.Books.Remove(book);

            _context.SaveChanges();
            return Ok();
        }
    }
}