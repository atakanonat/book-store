using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.AddBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.AddBook.AddBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

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
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context);
            var res = query.Handle(id);
            return Ok(res);
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
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand updateBook = new UpdateBookCommand(_context);
                updateBook.Handle(id, updatedBook);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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