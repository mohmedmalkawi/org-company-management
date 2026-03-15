using Application.Abstractions.Messaging;
using Application.Companies.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Companies;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("companies/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteCompanyCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteCompanyCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Companies);
    }
}
