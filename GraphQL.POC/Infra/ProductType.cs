using GraphQL.POC.Models;
using GraphQL.Types;

namespace GraphQL.POC.Infra
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            Name = "Product";

            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.Description);
            Field(x => x.Price);
            Field<CategoryType>(nameof(Product.Category));
        }
    }
}
