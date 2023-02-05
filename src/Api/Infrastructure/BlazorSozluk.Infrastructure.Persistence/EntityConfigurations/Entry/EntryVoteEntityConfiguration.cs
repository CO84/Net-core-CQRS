using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.Entry
{
    public  class EntryVoteEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.EntryVote>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("entry_vote", BlazorSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.Entry).WithMany(x => x.EntryVotes).HasForeignKey(x => x.EntryId);
        }
    }
}
