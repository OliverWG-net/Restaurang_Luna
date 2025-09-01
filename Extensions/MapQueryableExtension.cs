using Restaurang_luna.Extensions.Mappers;
using System.Data;

namespace Restaurang_luna.Extensions
{
    public static class MapQueryableExtension
    {
        public static IQueryable<TDto> AutoMap<TEntity, TDto>(this IQueryable<TEntity> query, IMapper<TEntity, TDto> map, DateTimeOffset now)
        {
            return query.Select(map.Projection(now));
        }
    }
}
