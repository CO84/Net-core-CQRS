﻿using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.EntryComment
{
    public class UserEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.User>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.User> builder)
        {
            base.Configure(builder);

            builder.ToTable("user", BlazorSozlukContext.DEFAULT_SCHEMA);     
            builder.Property(x => x.Email).HasColumnType("EmailAddress").IsRequired();
            builder.Property(x => x.Password).IsRequired();

            builder.HasData(new Api.Domain.Models.User
            {
                CreateDate = DateTime.Now,
                Email = "admin@gmail.com",
                EmailComfirmed = true,
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin",
                Password = "admin1234",
                Id = Guid.NewGuid(),
            });
        }
    }
}
