using WalletsMS.Contract;

namespace WalletsMS.Infrastructure
{
    public interface IWalletsService
    {
        Task<WalletDTO> AddNewWalletFromDTO(AddWalletDTO addWalletFromDTO);
        Task<WalletTypeDTO> AddNewWalletTypeFromDTO(AddWalletTypeDTO addWalletTypeFromDTO);
        Task<bool> DeleteWalletById(long id);
        Task<List<WalletDTO>> GetAllWalletsByUserId(long userId);
        Task<List<WalletTypeDTO>> GetAllWalletTypes();
    }
}