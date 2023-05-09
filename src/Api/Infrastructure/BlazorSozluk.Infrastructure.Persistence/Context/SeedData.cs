using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Infrastructure;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlazorSozluk.Infrastructure.Persistence.Context
{
    internal class SeedData
    {
        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.CreateDate, x => x.Date.Between(DateTime.Now.AddDays(-100),DateTime.Now))
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.Email, x => x.Person.Email)
                .RuleFor(x => x.UserName, x => x.Internet.UserName())
                .RuleFor(x => x.Password, x => PasswordEncryptor.Encrypt(x.Internet.Password()))
                .RuleFor(x => x.EmailComfirmed, x => x.PickRandom(true, false))
                .Generate(500);

            return result;
        }

        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(configuration["BlazorSozlukDbConnectionString"]);

            var context = new BlazorSozlukContext(dbContextBuilder.Options);

            if (context.Users.Any())
            {
                await Task.CompletedTask;
                return;
            }
            var users = GetUsers();
            var userIds = users.Select(x => x.Id);

            await context.Users.AddRangeAsync(users);

            var guids = Enumerable.Range(0, 150).Select(x => Guid.NewGuid()).ToList();
            int counter = 0;

            var entries = new Faker<Entry>("tr")
                .RuleFor(x => x.Id, x => guids[counter++])
                .RuleFor(x => x.CreateDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.Subject, x => x.Lorem.Sentence(5, 5))
                .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
                .RuleFor(x => x.CreatedById, x => x.PickRandom(userIds))
                .Generate(150);
            await context.Entries.AddRangeAsync(entries);

            var comments = new Faker<EntryComment>("tr")
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.CreateDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
                .RuleFor(x => x.CreatedById, x => x.PickRandom(userIds))
                .RuleFor(x => x.EntryId, x => x.PickRandom(guids))
                .Generate(1000);

            await context.EntryComments.AddRangeAsync(comments);

            await context.SaveChangesAsync();
        }
    }
}
