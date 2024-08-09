﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Contract
{
    public class ParsedTokenDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Platform { get; set; }
        public string? TokenAddress { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal UsdValue { get; set; }
        public WalletInfoDTO WalletInfo { get; set; }
    }
}
