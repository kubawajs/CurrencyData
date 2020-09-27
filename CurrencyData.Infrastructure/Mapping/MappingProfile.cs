using AutoMapper;
using CurrencyData.Infrastructure.Domain;
using EcbSdmx.Core.Domain;
using EcbSdmx.Core.Domain.Response;
using System;
using System.Linq;

namespace CurrencyData.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApiResponseData, ResponseData>()
                .ForMember(dest => dest.FromCurrency,
                    opt 
                        => opt.MapFrom(src => src.DataSet.Series.SeriesKey.Value.First(
                            x => x.Id == EcbSdmx.Infrastructure.Constants.EcbSdmx.CurrencyId).Value))
                .ForMember(dest => dest.ToCurrency,
                    opt 
                        => opt.MapFrom(src => src.DataSet.Series.SeriesKey.Value.First(
                            x => x.Id == EcbSdmx.Infrastructure.Constants.EcbSdmx.CurrencyDenomId).Value))
                .ForMember(dest => dest.StartDate,
                    opt => opt.MapFrom(src => Convert.ToDateTime(src.DataSet.Series.Observations.First().Dimension.Value)))
                .ForMember(dest => dest.EndDate,
                    opt => opt.MapFrom(src => Convert.ToDateTime(src.DataSet.Series.Observations.Last().Dimension.Value)))
                .ForMember(dest => dest.ExchangeRates,
                    opt => opt.MapFrom(src => src.DataSet.Series.Observations));
            CreateMap<Observation, DailyExchangeRate>()
                .ForMember(dest => dest.Date, opt
                    => opt.MapFrom(src => Convert.ToDateTime(src.Dimension.Value)))
                .ForMember(dest => dest.Rate, opt 
                    => opt.MapFrom(src => src.Value.Value));
            CreateMap<QueryParameters, EcbSdmxQueryParameters>();
        }
    }
}
