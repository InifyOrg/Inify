﻿using BlockchainParsersMS.Contract;
using TokensMS.Contract;

namespace BlockchainParsersMS.Infrastructure.Services
{
    public interface IWeb3Service
    {
        Task<List<ParsedTokenDTO>> parseBalancesWithMulticall(string address, List<TokenDTO> tokens);
        Task<ParsedTokenDTO> parseBaseErcToken(WalletDTO walletInfo);

    }
}