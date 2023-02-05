using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.Entry
{
    public  class EntryFavoriteEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.EntryFavorite>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryFavorite> builder)
        {
            base.Configure(builder);

            builder.ToTable("entry_favorite", BlazorSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.Entry).WithMany(x => x.EntryFavorites).HasForeignKey(x => x.EntryId);
            builder.HasOne(x => x.User).WithMany(x => x.EntryFavorites).HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
