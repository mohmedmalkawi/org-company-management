using Domain.Companies;
using Domain.Organizations;

namespace Infrastructure.Database;

public static class DataSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Organizations.Any())
        {
            return;
        }

        var org1Id = Guid.NewGuid();
        var org2Id = Guid.NewGuid();
        var org3Id = Guid.NewGuid();
        var org4Id = Guid.NewGuid();
        var org5Id = Guid.NewGuid();

        var organizations = new List<Organization>
        {
            new()
            {
                Id = org1Id,
                Name = "Tahaluf Al Emarat",
                Code = "TAE",
                Country = "United Arab Emirates",
                Phone = "+971-4-555-0001",
                FullAddress = "Dubai Internet City, Building 12, Dubai, UAE",
                CreationDate = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = org2Id,
                Name = "Global Tech Solutions",
                Code = "GTS",
                Country = "United States",
                Phone = "+1-415-555-0002",
                FullAddress = "100 Market Street, Suite 300, San Francisco, CA 94105",
                CreationDate = new DateTime(2024, 2, 20, 14, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = org3Id,
                Name = "European Digital Group",
                Code = "EDG",
                Country = "Germany",
                Phone = "+49-30-555-0003",
                FullAddress = "Friedrichstraße 123, 10117 Berlin, Germany",
                CreationDate = new DateTime(2024, 3, 10, 9, 15, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = org4Id,
                Name = "Asia Pacific Holdings",
                Code = "APH",
                Country = "Singapore",
                Phone = "+65-6555-0004",
                FullAddress = "1 Raffles Place, Tower 2, #40-02, Singapore 048616",
                CreationDate = new DateTime(2024, 4, 5, 8, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = org5Id,
                Name = "Middle East Innovations",
                Code = "MEI",
                Country = "Saudi Arabia",
                Phone = "+966-11-555-0005",
                FullAddress = "King Fahd Road, Al Olaya District, Riyadh 12211, Saudi Arabia",
                CreationDate = new DateTime(2024, 5, 1, 11, 45, 0, DateTimeKind.Utc)
            }
        };

        context.Organizations.AddRange(organizations);

        var companies = new List<Company>
        {
            new()
            {
                Id = Guid.NewGuid(),
                OrganizationId = org1Id,
                Name = "Tahaluf Software",
                Code = "TSW",
                Country = "United Arab Emirates",
                Phone = "+971-4-555-1001",
                FullAddress = "Dubai Internet City, Building 12, Floor 3, Dubai, UAE",
                CreationDate = new DateTime(2024, 1, 20, 10, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrganizationId = org1Id,
                Name = "Tahaluf Consulting",
                Code = "TCN",
                Country = "United Arab Emirates",
                Phone = "+971-4-555-1002",
                FullAddress = "Dubai Media City, Building 5, Floor 7, Dubai, UAE",
                CreationDate = new DateTime(2024, 1, 25, 12, 30, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrganizationId = org2Id,
                Name = "GTS Cloud Services",
                Code = "GCS",
                Country = "United States",
                Phone = "+1-415-555-2001",
                FullAddress = "200 California Street, Suite 400, San Francisco, CA 94111",
                CreationDate = new DateTime(2024, 2, 25, 9, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrganizationId = org2Id,
                Name = "GTS Data Analytics",
                Code = "GDA",
                Country = "United States",
                Phone = "+1-212-555-2002",
                FullAddress = "350 Fifth Avenue, Suite 2100, New York, NY 10118",
                CreationDate = new DateTime(2024, 3, 1, 15, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrganizationId = org3Id,
                Name = "EDG Cybersecurity",
                Code = "ECS",
                Country = "Germany",
                Phone = "+49-30-555-3001",
                FullAddress = "Potsdamer Platz 10, 10785 Berlin, Germany",
                CreationDate = new DateTime(2024, 3, 15, 10, 30, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrganizationId = org3Id,
                Name = "EDG Mobile Apps",
                Code = "EMA",
                Country = "France",
                Phone = "+33-1-555-3002",
                FullAddress = "25 Rue de la Paix, 75002 Paris, France",
                CreationDate = new DateTime(2024, 3, 20, 14, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrganizationId = org4Id,
                Name = "APH Fintech",
                Code = "AFT",
                Country = "Singapore",
                Phone = "+65-6555-4001",
                FullAddress = "8 Marina View, Asia Square Tower 1, #30-01, Singapore 018960",
                CreationDate = new DateTime(2024, 4, 10, 8, 30, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrganizationId = org4Id,
                Name = "APH E-Commerce",
                Code = "AEC",
                Country = "Japan",
                Phone = "+81-3-555-4002",
                FullAddress = "1-1 Marunouchi, Chiyoda-ku, Tokyo 100-0005, Japan",
                CreationDate = new DateTime(2024, 4, 15, 9, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrganizationId = org5Id,
                Name = "MEI Healthcare Tech",
                Code = "MHT",
                Country = "Saudi Arabia",
                Phone = "+966-11-555-5001",
                FullAddress = "King Abdullah Financial District, Tower 3, Riyadh, Saudi Arabia",
                CreationDate = new DateTime(2024, 5, 10, 10, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrganizationId = org5Id,
                Name = "MEI Smart Cities",
                Code = "MSC",
                Country = "United Arab Emirates",
                Phone = "+971-2-555-5002",
                FullAddress = "Abu Dhabi Global Market, Al Maryah Island, Abu Dhabi, UAE",
                CreationDate = new DateTime(2024, 5, 15, 11, 0, 0, DateTimeKind.Utc)
            }
        };

        context.Companies.AddRange(companies);
        context.SaveChanges();
    }
}
