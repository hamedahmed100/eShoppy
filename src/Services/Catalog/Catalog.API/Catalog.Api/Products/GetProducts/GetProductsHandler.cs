
using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts
{
    public record GetProductQuery() : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {

            // query from Martin librarry
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            // return
            return new GetProductResult(products);
        }
    }
}
