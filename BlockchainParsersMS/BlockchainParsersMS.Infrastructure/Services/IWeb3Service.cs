using BlockchainParsersMS.Contract;
using TokensMS.Contract;

namespace BlockchainParsersMS.Infrastructure.Services
{
    public interface IWeb3Service
    {
        Task<List<ParsedTokenDTO>> parseBalancesWithMulticall(List<string> addresses, List<TokenDTO> tokens);
        Task<ParsedTokenDTO> parseBaseErcToken(WalletMainInfoDTO walletInfo);

    }
}