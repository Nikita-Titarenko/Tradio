using AutoMapper;
using FluentResults;
using Tradio.Application.Dtos.Countries;
using Tradio.Application.Repositories;
using Tradio.Domain;

namespace Tradio.Application.Services.Countries
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
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
