using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureBase.Application.Common.Models;
using Nextended.Core.Extensions;
using Nextended.Core.Helper;

namespace CleanArchitectureBase.Application.Common.Extensions
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        public static IQueryable<T> QueryElementsTo<T>(
            this IQueryable queryable,
            ClassMappingSettings settings = null)
        {
            return queryable.ToEnumerable().MapElementsTo<T>(settings).AsQueryable();
        }
    }
}
