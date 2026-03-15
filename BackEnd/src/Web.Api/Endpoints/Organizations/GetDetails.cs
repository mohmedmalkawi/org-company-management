using Application.Abstractions.Messaging;
using Application.Organizations.GetDetails;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Organizations;

internal sealed class GetDetails : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("organizations/{id:guid}/details", async (
            Guid id,
            IQueryHandler<GetOrganizationDetailsQuery, OrganizationDetailsResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetOrganizationDetailsQuery(id);

            Result<OrganizationDetailsResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Organizations);
    }
}
