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
        Task<bool> DeleteWalletById(long id);
        Task<List<WalletDTO>> GetAllWalletsByUserId(long userId);

    }
}
