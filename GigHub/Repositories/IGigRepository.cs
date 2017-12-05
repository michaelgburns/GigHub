namespace GigHub.Repositories
{
    public interface IGigRepository
    {
        void Add(GigHub.Models.Gig gig);
        GigHub.Models.Gig GetGig(int gigId);
        System.Collections.Generic.IEnumerable<GigHub.Models.Gig> GetGigsUserAttending(string userId);
        GigHub.Models.Gig GetGigWithArtistWithGenre(int id);
        GigHub.Models.Gig GetGigWithAttendees(int gigId);
        System.Collections.Generic.IEnumerable<GigHub.Models.Gig> GetUpcommingGigsByArtist(string userId);
    }
}
