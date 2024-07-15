using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Client;
using UsersMS.Contracts;
using WalletsMS.Contract;
using WalletsMS.Infrastructure.Domain.Entities;

namespace WalletsMS.Infrastructure.Services
{
    public class WalletsService : IWalletsService
    {
        private readonly IWalletsDataLayer _walletsDataLayer;
        private readonly IUsersMsClient _usersService;

        public WalletsService(IWalletsDataLayer walletsDataLayer, IUsersMsClient usersMsClient)
        {
            _walletsDataLayer = walletsDataLayer;
            _usersService = usersMsClient;
        }

        public async Task<WalletDTO> AddNewWalletFromDTO(AddWalletDTO addWalletFromDTO)
        {
            UserDTO userDTO = await _usersService.GetUserByID(addWalletFromDTO.UserId);
            if (userDTO.Id > 0)
            {
                Wallet newWallet = addWalletFromDTO.Adapt<Wallet>();

                Wallet addedWallet = await _walletsDataLayer.AddWallet(newWallet, addWalletFromDTO.WalletTypeId);

                return addedWallet.Adapt<WalletDTO>();
            }
            return new WalletDTO();
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

        public async Task<List<WalletDTO>> GetAllWalletsByUserId(long userId)
        {
            List<Wallet> wallets = await _walletsDataLayer.GetWalletsById(userId);


            return wallets.Adapt<List<WalletDTO>>();
        }
    }
}
