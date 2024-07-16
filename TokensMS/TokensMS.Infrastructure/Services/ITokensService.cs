using TokensMS.Contract;

namespace TokensMS.Infrastructure
{
    public interface ITokensService
    {
        Task<PlatformDTO> AddNewPlatformFromDTO(AddPlatformDTO addPlatformDTO);
        Task<TokenDTO> AddNewTokenFromDTO(AddTokenDTO addTokenDTO);
        Task<WalletTypeDTO> AddNewWalletTypeFromDTO(AddWalletTypeDTO addWalletTypeDTO);
        Task<List<TokenDTO>> GetAllTokens();
        Task<List<TokenDTO>> GetAllTokensByWalletType(string walletType);
    }
}