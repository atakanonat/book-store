using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public int id;
        private readonly BookStoreDbContext _dbContext;
        public DeleteBookCommand(BookStoreDbContext dbContext)
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

            _dbContext.Books.Remove(book);

            _dbContext.SaveChanges();
        }
    }
}