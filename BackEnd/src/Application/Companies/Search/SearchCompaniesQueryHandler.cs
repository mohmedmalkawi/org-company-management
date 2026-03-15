using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Companies.Search;

internal sealed class SearchCompaniesQueryHandler(IApplicationDbContext context)
    : IQueryHandler<SearchCompaniesQuery, List<CompanySearchResponse>>
{
    public async Task<Result<List<CompanySearchResponse>>> Handle(
        SearchCompaniesQuery query,
        CancellationToken cancellationToken)
    {
        return await new SearchCompanyQueryBuilder(context)
           
            .FilterByOrganizationName(query.OrganizationName)
            
            .FilterByCompanyName(query.CompanyName)
            
            .FilterByCompanyCode(query.CompanyCode)
            
            .FilterByCountry(query.Country)
           
            .Paginate(query.Page, query.PageSize)
            
            .Build()
            
            .OrderBy(x => x.Company.Name)
            
            .Select(SearchCompanyMappings.ToSearchResponse)
            
            .ToListAsync(cancellationToken);
    }

}
