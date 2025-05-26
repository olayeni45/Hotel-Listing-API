using HotelListing.API.Data;
using Microsoft.AspNetCore.Mvc;


namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly List<Hotel> _hotels;
    public HotelsController()
    {
        _hotels = new List<Hotel>
        {
            new Hotel{Id = 1, Name = "Grand Plaza", Address = "123 Main st", Rating = 4.5},
            new Hotel{Id = 2, Name = "Ocean View", Address = "456 Beach Ad", Rating = 4.8},
        };
    }

    // GET: api/<HotelsController>
    [HttpGet]
    public ActionResult<IEnumerable<Hotel>> Get()
    {
        return Ok(_hotels);
    }

    // GET api/<HotelsController>/5
    [HttpGet("{id}")]
    public ActionResult<Hotel> Get(int id)
    {
        var hotel = _hotels.FirstOrDefault(x => x.Id == id);

        if (hotel == null)
            return NotFound();
        return Ok(hotel);
    }

    // POST api/<HotelsController>
    [HttpPost]
    public ActionResult<Hotel> Post([FromBody] Hotel request)
    {
        if (_hotels.Any(x => x.Id == request.Id))
            return BadRequest("Hotel with this Id already exists");

        _hotels.Add(request);
        return CreatedAtAction(nameof(Get), new { id = request.Id }, request);
    }

    // PUT api/<HotelsController>/
    [HttpPut]
    public ActionResult Put([FromBody] Hotel request)
    {
        var hotel = _hotels.FirstOrDefault(x => x.Id == request.Id);

        if (hotel == null)
        {
            return NotFound();
        }

        hotel.Name = request.Name;
        hotel.Address = request.Address;
        hotel.Rating = request.Rating;

        return NoContent();
    }

    // DELETE api/<HotelsController>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var hotel = _hotels.FirstOrDefault(x => x.Id == id);

        if (hotel == null)
        {
            return NotFound(new { message = "Hotel not found" });
        }

        _hotels.Remove(hotel);

        return NoContent();
    }
}