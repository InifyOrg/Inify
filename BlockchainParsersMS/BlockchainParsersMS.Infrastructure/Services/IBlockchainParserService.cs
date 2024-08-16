using BlockchainParsersMS.Contract;

namespace BlockchainParsersMS.Infrastructure
{
    public interface IBlockchainParserService
    {
        decimal getTotalBalance(List<ParsedTokenDTO> parsedTokens);
        BestTokenDTO getTotalBestSymbol(List<ParsedTokenDTO> parsedTokens);
        Task<List<ParsedTokenDTO>> parseOneByAddress(WalletMainInfoDTO walletInfo);
    }
}