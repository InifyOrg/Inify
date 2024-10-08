﻿using BlockchainParsersMS.Contract;
using Microsoft.Extensions.Configuration;
using Nethereum.Contracts;
using Nethereum.Contracts.Standards.ERC20.ContractDefinition;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TokensMS.Contract;

namespace BlockchainParsersMS.Infrastructure.Services
{
    public class Web3Service : IWeb3Service
    {
        private readonly Web3 _web3;
        public Web3Service()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _web3 = new Web3(configuration.GetSection("APIs:InfuraEthNode").Value);
        }

        public async Task<ParsedTokenDTO> ParseBaseErcToken(WalletDTO walletInfo)
        {
            ParsedTokenDTO result = new ParsedTokenDTO
            {
                Amount = Web3.Convert.FromWei(await _web3.Eth.GetBalance.SendRequestAsync(walletInfo.Address)),
                Platform = "ethereum",
                Slug = "ethereum",
                Name = "Ethereum",
                Symbol = "ETH",
                Chain = "ERC-20",
            };
            return result;
        }

        public async Task<List<ParsedTokenDTO>> ParseBalancesWithMulticall(string address, List<TokenDTO> tokens)
        {
            List<IMulticallInputOutput> callList = new List<IMulticallInputOutput>();

            foreach (var token in tokens)
            {
                BalanceOfFunction balanceOf = new BalanceOfFunction() { Owner = address };
                var call = new MulticallInputOutput<BalanceOfFunction, BalanceOfOutputDTO>(balanceOf, token.Address);
                callList.Add(call);
            }

            await _web3.Eth.GetMultiQueryHandler().MultiCallAsync(callList.ToArray()).ConfigureAwait(false);

            List<ParsedTokenDTO> currentBalances = new List<ParsedTokenDTO>();

            for(int i = 0; i < callList.Count; i++)
            {
                BigInteger balance = ((MulticallInputOutput<BalanceOfFunction, BalanceOfOutputDTO>)callList[i]).Output.Balance;
                if (balance > 0)
                {
                    ParsedTokenDTO balanceResult = new ParsedTokenDTO()
                    {
                        Name = tokens[i].Name,
                        Symbol = tokens[i].Symbol,
                        Platform = tokens[i].Platform.Slug,
                        Slug = tokens[i].Slug,
                        Chain = "ERC-20",
                        TokenAddress = tokens[i].Address,
                        Amount = Web3.Convert.FromWei(balance, tokens[i].Decimals),
                    };

                    currentBalances.Add(balanceResult);
                }
            }

            return currentBalances;
        }

    }
}
