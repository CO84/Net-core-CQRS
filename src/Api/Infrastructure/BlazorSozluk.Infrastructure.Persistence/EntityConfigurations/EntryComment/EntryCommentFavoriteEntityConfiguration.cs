using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.EntryComment
{
    public class EntryCommentFavoriteEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.EntryCommentFavorite>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryCommentFavorite> builder)
        {
            base.Configure(builder);

            builder.ToTable("entry_comment_favorite", BlazorSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.EntryComment).WithMany(x => x.EntryCommentFavorites).HasForeignKey(x => x.EntryCommentId);
            builder.HasOne(x => x.User).WithMany(x => x.EntryCommentFavorites).HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
