using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GameDot.Core.Exceptions;
using GameDot.Core.Services.RandomValueService;

namespace GameDot.Infrastructure.Services
{
    public class RandomValueService : IRandomValueService
    {
        private readonly HttpClient _httpClient;

        public RandomValueService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<RandomValueResult> GetRandomValueAsync()
        {
            try
            {
                ParsingRandomServiceResponse? res = await this._httpClient.GetFromJsonAsync<ParsingRandomServiceResponse>("");

                if (res == null)
                {
                    throw new GameDotException("0x7001", "Json response from OlegBelousovRandomService cannot be processed");
                }

                return new RandomValueResult
                {
                    Value = res.Random
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private class ParsingRandomServiceResponse
        {
            [JsonPropertyName("random")]
            public int Random { get; set; }
        }
    }
}
