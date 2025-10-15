using CoreLib.HttpService.Exceptions;
using CoreLib.HttpService.Services.Interfaces;
using CoreLib.HttpService.Services.Models;
using Services.External.Dto;
using Services.External.Interfaces;

namespace Services.External;

public sealed class GiftCatalogClient : IGiftCatalogClient
{
    private const string ClientName = "gift-catalog";
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(3);

    private readonly IHttpRequestService _http;
    
    ///
    public GiftCatalogClient(IHttpRequestService http)
    {
        _http = http;
    }
    
    /// <inheritdoc />
    public async Task<GiftCardResponse> GetCardByIdAsync(Guid cardId)
    {
        var request = new HttpRequestData
        {
            Method = HttpMethod.Get,
            Uri = new Uri($"/api/cards/{cardId}", UriKind.Relative)
        };

        var connection = new HttpConnectionData
        {
            ClientName = ClientName,
            Timeout = DefaultTimeout,
        };

        var httpResponse = await _http.SendRequestAsync<GiftCardResponse>(request, connection);

        if (httpResponse.Body is null)
            throw new HttpRequestExceptionEx(
                $"GiftCatalog returned empty body for card {cardId}",
                (int)httpResponse.StatusCode,
                responseBody: null
            );

        return httpResponse.Body;
    }
}