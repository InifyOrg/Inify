﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokensMS.Contract
{
    public class AddTokenDTO
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
        public string Address { get; set; }
        public AddPlatformDTO Platform { get; set; }
        public AddWalletTypeDTO WalletType { get; set; }

    }
}
