using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.EntryComment
{
    public class EmailConfirmationEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.EmailConfirmation>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.EmailConfirmation> builder)
        {
            base.Configure(builder);

            builder.ToTable("email_confirmation", BlazorSozlukContext.DEFAULT_SCHEMA);         
        }
    }
}
