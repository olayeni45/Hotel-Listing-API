namespace HotelListing.API.Data;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public IList<Hotel> Hotels { get; set; } = [];//Navigation Property 1:Many (1 country has many hotels)
}