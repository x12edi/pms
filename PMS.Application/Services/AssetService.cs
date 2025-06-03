using AutoMapper;
using PMS.Application.DTOs;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssetDto>> GetAllAssetsAsync()
        {
            var assets = await _unitOfWork.Assets.GetAllAsync();
            return _mapper.Map<IEnumerable<AssetDto>>(assets);
        }

        public async Task<AssetDto> GetAssetByIdAsync(int id)
        {
            var asset = await _unitOfWork.Assets.GetByIdAsync(id);
            if (asset == null)
                return null;
            return _mapper.Map<AssetDto>(asset);
        }

        public async Task<AssetDto> CreateAssetAsync(AssetDto assetDto)
        {
            var asset = _mapper.Map<Asset>(assetDto);
            asset.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Assets.AddAsync(asset);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<AssetDto>(asset);
        }

        public async Task UpdateAssetAsync(AssetDto assetDto)
        {
            var asset = await _unitOfWork.Assets.GetByIdAsync(assetDto.Id);
            if (asset == null)
                throw new KeyNotFoundException($"Asset with ID {assetDto.Id} not found.");
            _mapper.Map(assetDto, asset);
            await _unitOfWork.Assets.UpdateAsync(asset);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAssetAsync(int id)
        {
            var asset = await _unitOfWork.Assets.GetByIdAsync(id);
            if (asset == null)
                throw new KeyNotFoundException($"Asset with ID {id} not found.");
            await _unitOfWork.Assets.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}