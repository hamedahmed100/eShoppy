namespace Basket.API.Basket.GetBasket
{
    // public record GetBasketRequest(string UserName);
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                // This is shows that we are using MediatR
                var result = await sender.Send(new GetBasketQuery(userName));

                // This is showing that we are using Mapster
                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            }).WithName("GetItemById")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Item By Id")
                .WithDescription("Get item by id");
        }
    }
}
