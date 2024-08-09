using BlockchainParsersMS.Contract;

namespace BlockchainParsersMS.Infrastructure.Services
{
    public interface IWeb3Service
    {
        Task<ParsedTokenDTO> parseBaseErcToken(WalletInfoDTO walletInfo);
    }
}