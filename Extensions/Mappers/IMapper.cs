using System.Linq.Expressions;

namespace Restaurang_luna.Extensions.Mappers
{
    public interface IMapper<TEntity, TDto>
    {
        Expression<Func<TEntity, TDto>> Map{ get; }
    }
}
