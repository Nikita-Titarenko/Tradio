using AutoMapper;
using FluentResults;
using Tradio.Application.Dtos.Cities;
using Tradio.Application.Repositories;
using Tradio.Domain;

namespace Tradio.Application.Services.Cities
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CityDto>> GetCityAsync(int cityId)
        {
            var cityDbSet = _unitOfWork.GetDbSet<City>();

            var city = await cityDbSet.GetAsync(cityId);

            if (city == null)
            {
                return Result.Fail(new Error("City not found").WithMetadata("Code", "CityNotFound"));
            }

            return Result.Ok(_mapper.Map<CityDto>(city));
        }

        public async Task<Result<IEnumerable<CityDto>>> GetCitiesAsync(int countryId)
        {
            var cityRepository = _unitOfWork.GetCityRepository();

            var cities = await cityRepository.GetCitiesAsync(countryId);

            return Result.Ok(_mapper.Map<IEnumerable<CityDto>>(cities));
        }
    }
}
