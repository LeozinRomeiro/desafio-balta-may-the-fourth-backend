using Microsoft.AspNetCore.Mvc;
using StarLs.Application.Queries.Vehicles;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class VehicleEndpoints
    {
        public static void MapVehicleRoutes(this WebApplication app)
        {
            app.MapGet("/vehicles", async ([FromServices] IHandler<GetVehicleQueryRequest, List<GetVehicleQueryResponse>> handler) =>
            {
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
