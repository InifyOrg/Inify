﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokensMS.Contract
{
    public class TokenDTO
    {
        public long Id {  get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
        public string Address { get; set; }
        public int Decimals { get; set; }
        public PlatformDTO Platform { get; set; }
        public WalletTypeDTO WalletType { get; set; }
    }
}
