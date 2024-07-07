using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StarLs.Application.Queries;
using StarLs.Application.Queries.Vehicles;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class VehicleEndpoints
    {
        public static void MapVehicleRoutes(this WebApplication app)
        {
            app.MapGet("/vehicles", async ([FromServices] IHandler<GetVehicleQueryRequest, List<GetVehicleQueryResponse>> handler, [FromServices] IMemoryCache cache, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) =>
            {
                var result = new QueryResult<GetVehicleQueryResponse>(pageNumber, pageSize,
                    await cache.GetOrCreateAsync("VehiclesCache", async item =>
                    {
                        item.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
                        item.SlidingExpiration = TimeSpan.FromHours(12);

                        return await handler.Send(new GetVehicleQueryRequest());
                    })
                    );

                return Results.Ok(result);
            })
            .WithTags("Vehicle");

            app.MapGet("/vehicles/{id}", async ([FromServices] IHandler<GetVehicleByIdQueryRequest, GetVehicleByIdQueryResponse> handler,[FromRoute] short id) =>
            {
                var result = await handler.Send(new GetVehicleByIdQueryRequest(id));
                return Results.Ok(result);
            })
            .WithTags("Vehicle");
        }
    }
}
