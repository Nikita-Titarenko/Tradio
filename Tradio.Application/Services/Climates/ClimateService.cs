using AutoMapper;
using FluentResults;
using Tradio.Application.Dtos.Climates;
using Tradio.Application.Repositories;
using Tradio.Domain;

namespace Tradio.Application.Services.Climates
{
    public class ClimateService : IClimateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClimateService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> AddClimateAsync(ClimateDto dto, string userId)
        {
            var climateDbSet = _unitOfWork.GetDbSet<Climate>();
            var climate = _mapper.Map<ClimateDto, Climate>(dto);
            climate.CreationDateTime = DateTime.UtcNow;
            climate.UserId = userId;
            climateDbSet.Add(climate);

            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }

        public async Task<Result<ClimateStatisticDto?>> GetClimateStatisticAsync(string userId)
        {
            var climateRepository = _unitOfWork.GetClimateRepository();
            return Result.Ok(await climateRepository.GetClimateStatisticDtoAsync(userId));
        }
    }
}
