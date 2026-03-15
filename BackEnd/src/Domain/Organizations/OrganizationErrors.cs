using SharedKernel;

namespace Domain.Organizations;

public static class OrganizationErrors
{
    public static Error NotFound(Guid organizationId) =>
        Error.NotFound("Organizations.NotFound", $"The organization with Id = '{organizationId}' was not found");
}
