using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IGigRepository
    {
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigById(int GigId);
        IEnumerable<Gig> GetFutureActiveGigsByUser(string userId);
        Gig GetGigWithArtistByGigId(int id);
        void Add(Gig gig);
    }
}