using SharedKernel;

namespace Domain.Companies;

public static class CompanyErrors
{
    public static Error NotFound(Guid companyId) =>
        Error.NotFound("Companies.NotFound", $"The company with Id = '{companyId}' was not found");
}
