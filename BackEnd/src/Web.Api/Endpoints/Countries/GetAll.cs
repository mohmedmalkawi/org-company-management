namespace Web.Api.Endpoints.Countries;

internal sealed class GetAll : IEndpoint
{
    private static readonly string[] CountryList =
    [
        "Afghanistan", "Albania", "Algeria", "Argentina", "Australia",
        "Austria", "Bahrain", "Bangladesh", "Belgium", "Brazil",
        "Canada", "Chile", "China", "Colombia", "Czech Republic",
        "Denmark", "Egypt", "Finland", "France", "Germany",
        "Greece", "Hong Kong", "Hungary", "India", "Indonesia",
        "Iran", "Iraq", "Ireland", "Israel", "Italy",
        "Japan", "Jordan", "Kenya", "Kuwait", "Lebanon",
        "Malaysia", "Mexico", "Morocco", "Netherlands", "New Zealand",
        "Nigeria", "Norway", "Oman", "Pakistan", "Peru",
        "Philippines", "Poland", "Portugal", "Qatar", "Romania",
        "Russia", "Saudi Arabia", "Singapore", "South Africa", "South Korea",
        "Spain", "Sri Lanka", "Sweden", "Switzerland", "Taiwan",
        "Thailand", "Turkey", "Ukraine", "United Arab Emirates", "United Kingdom",
        "United States", "Vietnam"
    ];

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("countries", () => Results.Ok(CountryList))
            .WithTags(Tags.Countries);
    }
}
