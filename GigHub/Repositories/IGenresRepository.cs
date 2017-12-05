namespace GigHub.Repositories
{
    public interface IGenresRepository
    {
        System.Collections.Generic.IEnumerable<GigHub.Models.Genre> GetGenres();
    }
}
