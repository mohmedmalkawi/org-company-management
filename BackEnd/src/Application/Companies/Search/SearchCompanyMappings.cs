using System.Linq.Expressions;

namespace Application.Companies.Search;

public static class SearchCompanyMappings
{
    public static Expression<Func<CompanyWithOrganization, CompanySearchResponse>> ToSearchResponse =>
        x => new CompanySearchResponse
        {
            Id = x.Company.Id,
            OrganizationId = x.Company.OrganizationId,
            OrganizationName = x.Organization.Name,
            CompanyCode = x.Company.Code,
            CompanyName = x.Company.Name,
            Phone = x.Company.Phone
        };
}
