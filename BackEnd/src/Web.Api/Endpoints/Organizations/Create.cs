using Application.Abstractions.Messaging;
using Application.Organizations.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Organizations;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("organizations", async (
            Request request,
            ICommandHandler<CreateOrganizationCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateOrganizationCommand(
                request.Name,
                request.Code,
                request.Country,
                request.Phone,
                request.FullAddress);

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Organizations);
    }

    internal sealed record Request(string Name, string Code, string Country, string Phone, string FullAddress);
}
