
using MediatR;
using static BlazorSozluk.Common.Models.Enums;

namespace BlazorSozluk.Common.Models.RequestModels
{
    public class CreateEntryVoteCommand : IRequest<bool>
    {

        public Guid EntryId { get; set; }
        public Guid  CreatedById { get; set; }
        public VoteType VoteType { get; set; }

        public CreateEntryVoteCommand()
        {
        }
        public CreateEntryVoteCommand(Guid entryId, Guid createdById, VoteType voteType)
        {
            EntryId = entryId;
            CreatedById = createdById;
            VoteType = voteType;
        }
    }
}
