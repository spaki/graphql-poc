using GraphQL.POC.Repositories;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQL.POC.Infra
{
    public class ProductQuery : ObjectGraphType
    {
        public ProductQuery(IRepository repository)
        {
            Name = "ProductQuery";

            Field<ListGraphType<ProductType>>(
                "products",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IdGraphType> { Name = "id", },
                    new QueryArgument<StringGraphType> { Name = "name" },
                    new QueryArgument<StringGraphType> { Name = "description" },
                    new QueryArgument<DecimalGraphType> { Name = "minPrice" },
                    new QueryArgument<DecimalGraphType> { Name = "maxPrice" },
                }),
                resolve: context =>
                {
                    var query = repository.GetProductsQuery();

                    var id = context.GetArgument<Guid>("id");
                    if (id != Guid.Empty)
                        query = query.Where(e => e.Id == id);

                    var name = context.GetArgument<string>("name");
                    if (!string.IsNullOrWhiteSpace(name))
                        query = query.Where(e => e.Name.IndexOf(name, StringComparison.InvariantCultureIgnoreCase) > -1);

                    var description = context.GetArgument<string>("description");
                    if (!string.IsNullOrWhiteSpace(description))
                        query = query.Where(e => e.Description.IndexOf(description, StringComparison.InvariantCultureIgnoreCase) > -1);

                    var minPrice = context.GetArgument<decimal?>("minPrice");
                    if (minPrice.HasValue)
                        query = query.Where(e => e.Price >= minPrice.Value);

                    var maxPrice = context.GetArgument<decimal?>("maxPrice");
                    if (maxPrice.HasValue)
                        query = query.Where(e => e.Price <= maxPrice.Value);

                    return query.ToList();
                }
            );
        }
    }
}
