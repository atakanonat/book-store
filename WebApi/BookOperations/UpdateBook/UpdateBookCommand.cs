using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public int id;
        public UpdateBookModel updatedBook;
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Id == id);

            if (book is null)
            {
                throw new InvalidOperationException("The book doesn't exist.");
            }

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _dbContext.SaveChanges();
        }
        public class UpdateBookModel
        {
            public string? Title { get; set; }
            public int GenreId { get; set; }
        }
    }
}