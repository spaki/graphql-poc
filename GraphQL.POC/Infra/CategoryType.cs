using GraphQL.POC.Models;
using GraphQL.Types;

namespace GraphQL.POC.Infra
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Name = "Category";

            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);

            Field<ListGraphType<ProductType>>(nameof(Category.Products));
        }
    }
}
