using AutoMapper;

namespace CQRSPattern.Common.Builder.Mapper;

public class CustomMapper<TResponse> : ICustomMapper<TResponse>
{
    private readonly IMapper _mapper;
    private object _source;

    public CustomMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public ICustomMapper<TResponse> AddSource<TSource>(TSource source)
    {
        _source = source;
        return this;
    }

    public TResponse Map()
    {
        var response = Activator.CreateInstance<TResponse>();

        var typeMap = _mapper.ConfigurationProvider.BuildExecutionPlan(_source.GetType(), typeof(TResponse));
        if (typeMap is null)
            return response;

        response = _mapper.Map(_source, response);

        Reset();

        return response;
    }

    private void Reset()
    {
        _source = default;
    }
}