using BlockchainParsersMS.Contract;

namespace BlockchainParsersMS.Infrastructure
{
    public interface IBlockchainParserService
    {
        decimal GetTotalBalance(List<ParsedTokenDTO> parsedTokens);
        BestTokenDTO GetTotalBestSymbol(List<ParsedTokenDTO> parsedTokens);
        Task<ParsingOutputDTO> ParseManyByUserId(long userId);
        Task<List<ParsedTokenDTO>> ParseOneByAddress(WalletDTO walletInfo);
    }
}