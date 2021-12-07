using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle(int id, UpdateBookModel updatedBook)
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Id == id);

            if (book is null)
            {
                throw new InvalidOperationException("The book doesn't exist.");
            }

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _dbContext.SaveChanges();
        }
        public class UpdateBookModel
        {
            public string? Title { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
            public int GenreId { get; set; }
        }
    }
}