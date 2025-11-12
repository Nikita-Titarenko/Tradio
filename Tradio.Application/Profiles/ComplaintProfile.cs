using AutoMapper;
using Tradio.Application.Dtos.Complaints;
using Tradio.Domain;

namespace Tradio.Application.Profiles
{
    public class ComplaintProfile : Profile
    {
        public ComplaintProfile()
        {
            CreateMap<CreateComplaintDto, Complaint>();
            CreateMap<Complaint, ComplaintDto>();
        }
    }
}
