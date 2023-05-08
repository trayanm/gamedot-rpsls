namespace GameDot.Core.Services.RandomValueService
{
    public interface IRandomValueService
    {
        Task<RandomValueResult> GetRandomValueAsync();
    }
}
