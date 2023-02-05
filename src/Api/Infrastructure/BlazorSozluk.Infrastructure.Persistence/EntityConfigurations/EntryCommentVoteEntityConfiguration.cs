using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.EntryComment
{
    public class EntryCommentVoteEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.EntryCommentVote>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryCommentVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("entry_comment_vote", BlazorSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.EntryComment).WithMany(x => x.EntryCommentVotes).HasForeignKey(x => x.EntryCommentId);
        }
    }
}
