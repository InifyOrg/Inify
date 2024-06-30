using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletsMS.Contract;

namespace WalletsMS.Client
{
    public interface IWalletsMsClient
    {
        Task<WalletDTO> AddNewWalletFromDTO(AddWalletDTO addWalletFromDTO);
        Task<WalletTypeDTO> AddNewWalletTypeFromDTO(AddWalletTypeDTO addWalletTypeFromDTO);
        Task<bool> DeleteWalletById(long id);
        Task<WalletDTO> GetAllWalletsByUserId(long userId);

    }
}
