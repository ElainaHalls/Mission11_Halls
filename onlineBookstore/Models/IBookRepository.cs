namespace onlineBookstore.Models
{
    public interface IBookRepository
    {
        public IQueryable<Book> Books { get; }
    }
}
