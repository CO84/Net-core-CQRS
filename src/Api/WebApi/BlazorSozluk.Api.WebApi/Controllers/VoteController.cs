using BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote;
using BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BlazorSozluk.Common.Models.Enums;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : BaseController
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Entry/{entryId}")]
        public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await _mediator.Send(new CreateEntryVoteCommand(entryId, UserId, voteType));

            return Ok(result);
        }

        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await _mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, voteType, UserId));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVote(Guid entryId)
        {
            await _mediator.Send(new DeleteEntryVoteCommand(entryId, UserId));

            return Ok();
        }

        [HttpPost]
        [Route("DeleteEntryCommentVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
        {
             await _mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId));

            return Ok();
        }
    }
}
