using AutoMapper;
using PMS.Application.DTOs;
using PMS.Domain.Entities;

namespace PMS.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Portfolio
            CreateMap<Portfolio, PortfolioDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? $"{src.Client.FirstName} {src.Client.LastName}" : null))
                .ReverseMap();

            // Client
            CreateMap<Client, ClientDto>().ReverseMap();

            // Asset
            CreateMap<Asset, AssetDto>().ReverseMap();

            // Holding
            CreateMap<Holding, HoldingDto>()
                .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset != null ? src.Asset.Name : null))
                .ReverseMap();

            // Transaction
            CreateMap<Transaction, TransactionDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id when mapping DTO to entity

            // Performance
            CreateMap<Performance, PerformanceDto>().ReverseMap();

            // Benchmark
            CreateMap<Benchmark, BenchmarkDto>().ReverseMap();

            // Account
            CreateMap<Account, AccountDto>().ReverseMap();

            // Goal
            CreateMap<Goal, GoalDto>().ReverseMap();

            // Allocation
            CreateMap<Allocation, AllocationDto>().ReverseMap();

            // User
            CreateMap<User, UserDto>().ReverseMap();

            // Report
            CreateMap<Report, ReportDto>().ReverseMap();

            // PriceHistory
            CreateMap<PriceHistory, PriceHistoryDto>().ReverseMap();
        }
    }
}