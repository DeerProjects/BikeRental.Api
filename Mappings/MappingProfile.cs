
using AutoMapper;
using BikeRental.Api;
namespace BikeRental.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Bicycle mappings
        CreateMap<Models.Bicycle, BicycleReadDto>();
        CreateMap<BicycleCreateDto, Models.Bicycle>()
            .ForMember(d => d.IsAvailable, o => o.MapFrom(_ => true))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTimeOffset.UtcNow));
        CreateMap<BicycleUpdateDto, Models.Bicycle>();

        //Customer mappings
        CreateMap<Models.Customer, CustomerReadDto>();
        CreateMap<CustomerCreateDto, Models.Customer>()
            .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTimeOffset.UtcNow));
        CreateMap<CustomerUpdateDto, Models.Customer>();

        //Rental mappings
        CreateMap<Models.Rental, RentalReadDto>();
        //Handle fields in service , not via mapping

    }
}