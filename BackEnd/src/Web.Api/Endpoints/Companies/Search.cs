using Application.Abstractions.Messaging;
using Application.Companies.Search;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Companies;

internal sealed class Search : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("companies", async (
            string? organizationName,
            string? companyName,
            string? country,
            string? companyCode,
            int? page,
            int? pageSize,
            IQueryHandler<SearchCompaniesQuery, List<CompanySearchResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new SearchCompaniesQuery(
                organizationName,
                companyName,
                country,
                companyCode,
                page,
                pageSize);

            Result<List<CompanySearchResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Companies);
    }
}
