using GraphQL.POC.Models;
using System.Linq;

namespace GraphQL.POC.Repositories
{
    public interface IRepository
    {
        IQueryable<Product> GetProductsQuery();
        IQueryable<Category> GetCategoryQuery();
    }
}
