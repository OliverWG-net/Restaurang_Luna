using Restaurang_luna.Extensions.Mappers;

namespace Restaurang_luna.Extensions
{
    public static class MapQueryableExtension
    {
        public static IQueryable<TDto> AutoMap<TEntity, TDto>(this IQueryable<TEntity> query, IMapper<TEntity, TDto> map)
        {
            return query.Select(map.Map);
        }
    }
}
