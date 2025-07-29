using AutoMapper;
using Market.Application.DTOs.Measurement;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class MeasurementProfile : Profile
    {

        public MeasurementProfile()
        {
            CreateMap<Measurement, MeasurementRequest>()
            .ReverseMap();

            CreateMap<Measurement, MeasurementUpdateRequest>()
                    .ReverseMap();

            CreateMap<Measurement, MeasurementResponse>()
                .ReverseMap();
        }
    }
}
