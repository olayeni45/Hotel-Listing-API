using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.Hotel;
using Microsoft.AspNetCore.Mvc;


namespace HotelListing.API.Controllers;

public class HotelsController : ApiControllerBase
{
    private readonly IMapper _mapper;
    private readonly IHotelsRepository _hotelsRepository;

    public HotelsController(IMapper mapper, IHotelsRepository hotelsRepository)
    {
        _mapper = mapper;
        _hotelsRepository = hotelsRepository;
    }

    // GET: api/<HotelsController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HotelDto>>> Get()
    {
        var hotels = await _hotelsRepository.GetAllAsync();
        var records = _mapper.Map<List<HotelDto>>(hotels);
        return Ok(records);
    }

    // GET api/<HotelsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<HotelDto>> Get(int id)
    {
        var hotel = await _hotelsRepository.GetAsync(id);

        if (hotel == null)
            return NotFound();

        var record = _mapper.Map<HotelDto>(hotel);
        return Ok(record);
    }

    // POST api/<HotelsController>
    [HttpPost]
    public async Task<ActionResult<Hotel>> Post([FromBody] CreateHotelDto request)
    {
        var entity = _mapper.Map<Hotel>(request);
        await _hotelsRepository.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, _mapper.Map<HotelDto>(entity));
    }

    // PUT api/<HotelsController>/
    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] HotelDto request)
    {
        if (id != request.Id)
            return BadRequest();

        var entity = await _hotelsRepository.GetAsync(request.Id);

        if (entity == null)
        {
            return NotFound();
        }

        _mapper.Map(request, entity);
        await _hotelsRepository.UpdateAsync(entity);

        return NoContent();
    }

    // DELETE api/<HotelsController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var hotel = await _hotelsRepository.GetAsync(id);

        if (hotel == null)
        {
            return NotFound(new { message = "Hotel not found" });
        }

        await _hotelsRepository.DeleteAsync(id);

        return NoContent();
    }
}