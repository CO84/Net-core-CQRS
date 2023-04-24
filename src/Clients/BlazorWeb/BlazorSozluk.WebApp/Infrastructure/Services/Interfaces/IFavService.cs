namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public interface IFavService
    {
        Task CreateEntryCommentFav(Guid entryCommentId);
        Task CreateEntryFav(Guid entryId);
        Task DeleteEntryCommentFav(Guid entryCommentId);
        Task DeleteEntryFav(Guid entryId);
    }
}