using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        public int id;
        private readonly BookStoreDbContext _bookStoreDbContext;
        public GetBookByIdQuery(BookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }
        public ByIdBookModel Handle()
        {
            var book = _bookStoreDbContext.Books.First(book => book.Id == id);
            if (book is null)
            {
                throw new InvalidOperationException("The book doesn't exist.");
            }
            var vmBook = new ByIdBookModel()
            {
                Title = book.Title,
                PageCount = book.PageCount,
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                Genre = ((GenreEnum)book.GenreId).ToString()
            };
            return vmBook;
        }
        public class ByIdBookModel
        {
            public string? Title { get; set; }
            public int PageCount { get; set; }
            public string? PublishDate { get; set; }
            public string? Genre { get; set; }
        }
    }
}