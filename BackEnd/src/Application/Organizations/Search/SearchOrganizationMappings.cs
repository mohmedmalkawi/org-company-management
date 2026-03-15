using System.Linq.Expressions;
using Domain.Organizations;

namespace Application.Organizations.Search;

internal sealed class SearchOrganizationMappings
{
    public static Expression<Func<Organization, OrganizationSearchResponse>> ToSearchResponse =>
            org => new OrganizationSearchResponse
            {
                Id = org.Id,
                Code = org.Code,
                Name = org.Name,
                Phone = org.Phone
            };
}
