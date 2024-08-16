using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Contract
{
    public class ParsingOutputDTO
    {
        public string TotalBestTokenSymbol { get; set; }
        public decimal TotalBalance { get; set; }

        public List<WalletParsedInfoDTO> walletInfos { get; set; }

        public List<ParsedTokenDTO> Tokens { get; set; }

    }
}
