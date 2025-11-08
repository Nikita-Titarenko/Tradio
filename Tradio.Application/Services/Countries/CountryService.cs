using AutoMapper;
using Eventa.Application.Repositories;
using FluentResults;
using Tradio.Application.Dtos.Countries;
using Tradio.Application.Services.Countries;
using Tradio.Domain;

namespace Tradio.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CountryDto>>> GetCountriesAsync()
        {
            var countryDbSet = _unitOfWork.GetDbSet<Country>();
            var countries = await countryDbSet.GetAllAsync();

            return Result.Ok(_mapper.Map<IEnumerable<CountryDto>>(countries));
        }
    }
}
