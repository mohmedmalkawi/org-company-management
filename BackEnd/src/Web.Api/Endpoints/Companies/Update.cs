using Application.Abstractions.Messaging;
using Application.Companies.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Companies;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("companies/{id:guid}", async (
            Guid id,
            Request request,
            ICommandHandler<UpdateCompanyCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateCompanyCommand(
                id,
                request.OrganizationId,
                request.Name,
                request.Code,
                request.Country,
                request.Phone,
                request.FullAddress);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Companies);
    }

    internal sealed record Request(
        Guid OrganizationId,
        string Name,
        string Code,
        string Country,
        string Phone,
        string FullAddress);
}
