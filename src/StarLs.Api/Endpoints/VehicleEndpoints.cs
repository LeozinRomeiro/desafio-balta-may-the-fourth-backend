using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StarLs.Application.Queries.Vehicles;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class VehicleEndpoints
    {
        public static void MapVehicleRoutes(this WebApplication app)
        {
            app.MapGet("/vehicles", async ([FromServices] IHandler<GetVehicleQueryRequest, List<GetVehicleQueryResponse>> handler, [FromServices] IMemoryCache cache) =>
            {
                var memoryCache = cache.GetOrCreate("VehiclesCache", item =>
                {
                    item.SlidingExpiration = TimeSpan.FromHours(1);
                    return DateTime.Now;
                });
                var result = await handler.Send(new GetVehicleQueryRequest());
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
