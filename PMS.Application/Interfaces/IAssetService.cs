using PMS.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IAssetService
    {
        Task<IEnumerable<AssetDto>> GetAllAssetsAsync();
        Task<AssetDto> GetAssetByIdAsync(int id);
        Task<AssetDto> CreateAssetAsync(AssetDto assetDto);
        Task UpdateAssetAsync(AssetDto assetDto);
        Task DeleteAssetAsync(int id);
    }
}