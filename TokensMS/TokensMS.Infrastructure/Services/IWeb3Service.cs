namespace TokensMS.Infrastructure
{
    public interface IWeb3Service
    {
        Task<int> GetDecimalsByAddress(string address);
    }
}