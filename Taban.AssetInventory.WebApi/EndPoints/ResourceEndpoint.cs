using FluentResults;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Taban.AssetInventory.Application.Resources.Commands;
using Taban.AssetInventory.Infrastructure.DataGrid;
using Taban.AssetInventory.Application.Resources.Queries;

namespace Taban.AssetInventory.WebApi.Endpoints;

public static class ResourceEndpoint
{
    public static void MapResourceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/resource").WithTags("Resource");

        group.MapPost("/data-table", async (
            DataGridRequest gridRequest,
            ISender mediator,
            CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetResourceDataTableQuery(gridRequest), cancellationToken);

            return result;
        });

        group.MapGet("/{id:long}", async (
            long id,
            ISender mediator,
            CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetResourceQuery(id), cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(result.Errors.First().Message);
        });

        group.MapGet("/select-items", async (
            ISender mediator,
            CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetResourceSelectItemsQuery(), cancellationToken);

            return result;
        });

        group.MapPost("create", async (
                CreateResourceCommand command,
                ISender sender) =>
        {
            var result = await sender.Send(command);

            return Results.Ok(result);
        })
            .WithName("CreateResource")
            .Produces<Result<CreateResourceResponse>>();


        group.MapPost("update", async (
                UpdateResourceCommand command,
                ISender sender) =>
        {
            var result = await sender.Send(command);

            return Results.Ok(result);
        })
            .WithName("UpdateResource")
            .Produces<Result<UpdateResourceResponse>>();

        group.MapPost("delete", async (
                DeleteResourcesCommand command,
                ISender sender) =>
        {
            var result = await sender.Send(command);

            return Results.Ok(result);
        })
            .WithName("DeleteResource")
            .Produces<Result<List<DeleteResourceResponse>>>();
    }
}
