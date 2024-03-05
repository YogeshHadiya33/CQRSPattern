namespace CQRSPattern.Common.Builder.Mapper;

public interface ICustomMapper<out TResponse>
{
    ICustomMapper<TResponse> AddSource<TSource>(TSource source);
    TResponse Map();
}