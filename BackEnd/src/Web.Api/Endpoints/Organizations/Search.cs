using Application.Abstractions.Messaging;
using Application.Organizations.Search;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Organizations;

internal sealed class Search : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("organizations", async (
            string? Name,
            string? Code,
            string? Country,
            int? Page,
            int? PageSize,
            IQueryHandler<SearchOrganizationsQuery, List<OrganizationSearchResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new SearchOrganizationsQuery(Name, Code, Country, Page, PageSize);

            Result<List<OrganizationSearchResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Organizations);
    }
}
