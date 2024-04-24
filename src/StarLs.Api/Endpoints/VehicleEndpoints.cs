using Microsoft.AspNetCore.Mvc;
using StarLs.Application.Queries.Vehicles;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class VehicleEndpoints
    {
        public static void MapVehicleRoutes(this WebApplication app)
        {
            app.MapGet("/vehicles", ([FromServices] IHandler<GetVehicleQueryRequest, List<GetVehicleQueryResponse>> handler) =>
            {
                return Results.Ok(handler.Send(new GetVehicleQueryRequest()));
            });

            app.MapGet("/vehicles/{id}", ([FromServices] IHandler<GetVehicleByIdQueryRequest, List<GetVehicleByIdQueryResponse>> handler,[FromRoute] short id) =>
            {
                return Results.Ok(handler.Send(new GetVehicleByIdQueryRequest(id)));
            });
        }
    }
}
