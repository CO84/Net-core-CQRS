﻿using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient _httpClient;

        public EntryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GetEntriesViewModel>> GetEntries()
        {
            var result = await _httpClient.GetFromJsonAsync<List<GetEntriesViewModel>>("api/entry?todayEntries=false&count=30");
            return result;
        }

        public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
        {
            var result = await _httpClient.GetFromJsonAsync<GetEntryDetailViewModel>($"api/entry/{entryId}");
            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
        {
            var result = await _httpClient.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"api/entry/mainpageentries?page={page}&pagesize={pageSize}");
            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
        {
            var result = await _httpClient.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"api/entry/UserEntries?userName={userName}&page={page}&pagesize={pageSize}");
            return result;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
        {
            var result = await _httpClient.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"api/entry/comment/{entryId}?page={page}&pagesize={pageSize}");
            return result;
        }

        public async Task<Guid> CreateEntry(CreateEntryCommand command)
        {
            var res = await _httpClient.PostAsJsonAsync("api/entry/CreateEntry", command);
            if (!res.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await res.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
        {
            var res = await _httpClient.PostAsJsonAsync("api/entry/CreateEntryComment", command);
            if (!res.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await res.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<List<SearchEntryViewModel>> SearchSubject(string searchText)
        {
            var result = await _httpClient.GetFromJsonAsync<List<SearchEntryViewModel>>($"api/entry/Search?searchText={searchText}");
            return result;
        }
    }
}
