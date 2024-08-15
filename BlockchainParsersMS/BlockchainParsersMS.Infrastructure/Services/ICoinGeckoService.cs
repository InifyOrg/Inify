namespace BlockchainParsersMS.Infrastructure
{
    public interface ICoinGeckoService
    {
        Task<decimal> GetPriceByCoinId(string id);
        Task<decimal> GetPriceByTokenAddress(string address, string platform);
    }
}