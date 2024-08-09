using BlockchainParsersMS.Contract;

namespace BlockchainParsersMS.Infrastructure
{
    public interface IBlockchainParserService
    {
        Task<List<ParsedTokenDTO>> parseOneByAddress(WalletInfoDTO walletInfo);
    }
}