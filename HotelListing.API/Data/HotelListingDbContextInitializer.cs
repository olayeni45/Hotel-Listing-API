using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data;

public class HotelListingDbContextInitializer
{
    private readonly HotelListingDbContext _context;
    private readonly ILogger<HotelListingDbContextInitializer> _logger;

    public HotelListingDbContextInitializer(HotelListingDbContext context, ILogger<HotelListingDbContextInitializer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occured while initializing the database: {ex}");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occured while seeding the database: {ex}");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        var nigeria = new Country { Name = "Nigeria", ShortName = "NG" };
        var ghana = new Country { Name = "Ghana", ShortName = "GH" };
        var countries = new List<Country> { nigeria, ghana };

        if (!_context.Countries.Any())
        {
            await _context.Countries.AddRangeAsync(countries);
            await _context.SaveChangesAsync();
        }

        var hotels = new List<Hotel>
        {
            new Hotel{Name = "Sheraton", Address="Ikeja", CountryId = nigeria.Id, Rating = 4.6 },
            new Hotel{Name = "Four Points", Address="Kumasi Main", CountryId = ghana.Id, Rating = 3.4 },
        };

        if (!_context.Hotels.Any())
        {
            await _context.Hotels.AddRangeAsync(hotels);
            await _context.SaveChangesAsync();
        }

        var roles = new List<IdentityRole>()
        {
            new (){Name = Constants.Roles.Admin, NormalizedName = Constants.Roles.Admin.ToUpper()},
            new (){Name = Constants.Roles.User, NormalizedName = Constants.Roles.User.ToUpper()},
        };

        if (!_context.Roles.Any())
        {
            await _context.Roles.AddRangeAsync(roles);
            await _context.SaveChangesAsync();
        }
    }
}
