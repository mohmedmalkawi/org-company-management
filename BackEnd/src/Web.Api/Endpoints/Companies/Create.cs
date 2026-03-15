using Application.Abstractions.Messaging;
using Application.Companies.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Companies;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("companies", async (
            Request request,
            ICommandHandler<CreateCompanyCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateCompanyCommand(
                request.OrganizationId,
                request.Name,
                request.Code,
                request.Country,
                request.Phone,
                request.FullAddress);

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
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
