using WebApi.DBOperations;

namespace WebApi.BookOperations.AddBook
{
    public class AddBookCommand
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        public AddBookModel addBookModel { get; set; }
        public AddBookCommand(BookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }
        public void Handle()
        {
            var book = _bookStoreDbContext.Books.SingleOrDefault(book => book.Title == addBookModel.Title);

            if (book is not null)
            {
                throw new InvalidOperationException("This book already exist.");
            }
            book = new Book()
            {
                Title = addBookModel.Title,
                PageCount = addBookModel.PageCount,
                PublishDate = addBookModel.PublishDate,
                GenreId = addBookModel.GenreId
            };
            _bookStoreDbContext.Books.Add(book);
            _bookStoreDbContext.SaveChanges();
        }
        public class AddBookModel
        {
            public string? Title { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
            public int GenreId { get; set; }
        }
    }
}