using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletsMS.Contract;
using WalletsMS.Infrastructure.Domain.Entities;

namespace WalletsMS.Infrastructure.Services
{
    public class WalletsService : IWalletsService
    {
        private readonly IWalletsDataLayer _walletsDataLayer;

        public WalletsService(IWalletsDataLayer walletsDataLayer)
        {
            _walletsDataLayer = walletsDataLayer;
        }

        public async Task<WalletDTO> AddNewWalletFromDTO(AddWalletDTO addWalletFromDTO)
        {
            Wallet newWallet = addWalletFromDTO.Adapt<Wallet>();

            Wallet addedWallet = await _walletsDataLayer.AddWallet(newWallet);

            return addedWallet.Adapt<WalletDTO>();
        }

        public async Task<WalletTypeDTO> AddNewWalletTypeFromDTO(AddWalletTypeDTO addWalletTypeFromDTO)
        {
            WalletType newWalletType = addWalletTypeFromDTO.Adapt<WalletType>();

            WalletType addedWalletType = await _walletsDataLayer.AddWalletType(newWalletType);

            return addedWalletType.Adapt<WalletTypeDTO>();
        }

        public async Task<bool> DeleteWalletById(long id)
        {
            return await _walletsDataLayer.DeleteWalletById(id);
        }

        public async Task<WalletDTO> GetAllWalletsByUserId(long userId)
        {
            Wallet wallet = await _walletsDataLayer.GetWalletById(userId);

            return wallet.Adapt<WalletDTO>();
        }
    }
}
