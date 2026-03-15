using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Organizations.Search;

internal sealed class SearchOrganizationsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<SearchOrganizationsQuery, List<OrganizationSearchResponse>>
{
    public async Task<Result<List<OrganizationSearchResponse>>> Handle(
        SearchOrganizationsQuery query,
        CancellationToken cancellationToken)
    {
        return await new SearchOrganizationQueryBuilder(context)
            .FilterByName(query.Name)
            .FilterByCode(query.Code)
            .FilterByCountry(query.Country)
            .ApplyPaging(query.Page, query.PageSize)
            .Build()
            .OrderBy(o => o.Name)
            .Select(SearchOrganizationMappings.ToSearchResponse)
            .ToListAsync(cancellationToken);
    }
}
