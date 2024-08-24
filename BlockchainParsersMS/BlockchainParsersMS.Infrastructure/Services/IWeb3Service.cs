using BlockchainParsersMS.Contract;
using TokensMS.Contract;

namespace BlockchainParsersMS.Infrastructure.Services
{
    public interface IWeb3Service
    {
        Task<List<ParsedTokenDTO>> ParseBalancesWithMulticall(string address, List<TokenDTO> tokens);
        Task<ParsedTokenDTO> ParseBaseErcToken(WalletDTO walletInfo);

    }
}