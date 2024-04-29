using AutoMapper;
using StarLs.Tests.Builders.Mapper;

namespace StarLs.Tests;
public abstract class BaseTests
{
    protected IMapper? mapper;

    public IMapper GetMapper() =>
        MockMapper.Build();
}
