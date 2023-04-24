using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepository;

        public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();
            if (request.UserId is not null && request.UserId.HasValue && request.UserId != Guid.Empty)
            {
                query = query.Where(x => x.CreatedById == request.UserId);
            }
            else if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(x => x.CreatedBy.UserName == request.UserName);
            }
            else return null;

            query = query.Include(x => x.EntryFavorites)
                         .Include(x => x.CreatedBy);

            var list = query.Select(x => new GetUserEntriesDetailViewModel()
            {
                Id = x.Id,
                Subject = x.Subject,
                Content = x.Content,
                IsFavorited = false,
                CreatedByUserName = request.UserName,
                CreatedDate = x.CreateDate,
                FavoritedCount = x.EntryFavorites.Count
            });

            var entries = await list.GetPaged(request.Page, request.PageSize);

            return entries;
        }
    }
}
