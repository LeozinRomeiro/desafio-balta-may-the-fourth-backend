using AutoMapper;
using StarLs.Application.Mappings;

namespace StarLs.Tests.Builders.Mapper;
public class MockMapper
{
    private static IMapper _mapper = null!;

    public static IMapper Build()
    {
        MapperConfiguration confMapper = new MapperConfiguration(conf =>
        {
            conf.AddProfile(new AutoMapperProfile());
        });

        _mapper = confMapper.CreateMapper();

        return _mapper;
    }
}
