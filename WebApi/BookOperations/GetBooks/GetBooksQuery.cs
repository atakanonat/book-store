using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        public GetBooksQuery(BookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }
        public List<BookViewModel> Handle()
        {
            var bookList = _bookStoreDbContext.Books.OrderBy(book => book.Id).ToList<Book>();
            List<BookViewModel> vm = new List<BookViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BookViewModel()
                {
                    Title = book.Title,
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                    Genre = ((GenreEnum)book.GenreId).ToString()
                });
            }
            return vm;
        }
    }
    public class BookViewModel
    {
        public string? Title { get; set; }
        public int PageCount { get; set; }
        public string? PublishDate { get; set; }
        public string? Genre { get; set; }
    }
}