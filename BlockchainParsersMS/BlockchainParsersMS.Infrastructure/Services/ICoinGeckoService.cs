namespace BlockchainParsersMS.Infrastructure
{
    public interface ICoinGeckoService
    {
        Task<decimal> GetPriceByCoinId(string id);
    }
}