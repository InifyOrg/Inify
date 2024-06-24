using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletsMS.Contract;

namespace WalletsMS.Infrastructure.Services
{
    public class WalletsService : IWalletsService
    {
        private readonly IWalletsDataLayer _walletsDataLayer;

        public WalletsService(IWalletsDataLayer walletsDataLayer)
        {
            _walletsDataLayer = walletsDataLayer;
        }

        public Task<WalletDTO> AddNewWalletFromDTO(AddWalletDTO addWalletFromDTO)
        {
            throw new NotImplementedException();
        }

        public Task<WalletTypeDTO> AddNewWalletTypeFromDTO(AddWalletTypeDTO addWalletTypeFromDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteWalletById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<WalletDTO> GetAllWalletsByUserId(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
