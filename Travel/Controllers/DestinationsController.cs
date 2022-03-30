using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Travel.Models;

namespace Travel.Controllers
{
  [Route("api/Destinations")]
  [ApiController]
  public class DestinationsController : ControllerBase
  {
    private readonly TravelContext _db;

    public DestinationsController(TravelContext db)
    {
      _db = db;
    }

    // GET: api/Destinations

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] UrlQuery urlQuery)
    {
        var validUrlQuery = new UrlQuery(urlQuery.PageNumber, urlQuery.PageSize);
        var pagedData = _db.Destinations
          .OrderBy(thing => thing.DestinationId)
          .Skip((validUrlQuery.PageNumber - 1) * validUrlQuery.PageSize)
          .Take(validUrlQuery.PageSize);
        return Ok(pagedData);
    }

     //GET: api/Destinations/Country?country=
    [HttpGet]
    [Route("Country")]
    public async Task<ActionResult<IEnumerable<Review>>> Country(string country)
    {
      var query = _db.Reviews.AsQueryable();

      if (country != null)
      {
        var id = _db.Destinations.Where(x => x.Country == country).ToList();
        List<int> www = new List<int> {};
        foreach (var thing in id) 
        {
          www.Add(thing.DestinationId);
        }
        query = query.Where(r => www.Contains(r.DestinationId));
      }
      return await query.ToListAsync();
    }

     //GET: api/Destinations/City
    [HttpGet]
    [Route("City")]
    public async Task<ActionResult<IEnumerable<Review>>> City(string city)
    {
      var query = _db.Reviews.AsQueryable();

      if (city != null)
      {
        var id = _db.Destinations.Where(x => x.City == city).ToList();
        List<int> www = new List<int> {};
        foreach (var thing in id) 
        {
          www.Add(thing.DestinationId);
        }
        query = query.Where(r => www.Contains(r.DestinationId));
      }
      return await query.ToListAsync();
    }

    // GET: api/Destinations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Destination>> GetDestination(int id)
    {
        var destination = await _db.Destinations.FindAsync(id);

        if (destination == null)
        {
            return NotFound();
        }

        return destination;
    }

    // PUT: api/Destinations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Destination destination)
    {
      if (id != destination.DestinationId)
      {
        return BadRequest();
      }

      _db.Entry(destination).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!DestinationExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Destinations
    [HttpPost]
    public async Task<ActionResult<Destination>> Post(Destination destination)
    {
      _db.Destinations.Add(destination);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetDestination), new { id = destination.DestinationId }, destination);
    }

    // DELETE: api/Destinations/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDestination(int id)
    {
      var destination = await _db.Destinations.FindAsync(id);
      if (destination == null)
      {
        return NotFound();
      }

      _db.Destinations.Remove(destination);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool DestinationExists(int id)
    {
      return _db.Destinations.Any(e => e.DestinationId == id);
    }
  }

  [ApiVersion("2.0")]
  [Route("api/{v:apiVersion}/Destinations")]
  [ApiController]
  public class DestinationsV2Controller : ControllerBase
  {
    private readonly TravelContext _db;

    public DestinationsV2Controller(TravelContext db)
    {
      _db = db;
    }

    // GET: api/Destinations

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] UrlQuery urlQuery)
    {
        var validUrlQuery = new UrlQuery(urlQuery.PageNumber, urlQuery.PageSize);
        var pagedData = _db.Destinations
          .OrderBy(thing => thing.DestinationId)
          .Skip((validUrlQuery.PageNumber - 1) * validUrlQuery.PageSize)
          .Take(validUrlQuery.PageSize);
        return Ok(pagedData);
    }

     //GET: api/Destinations/Country?country=
    [HttpGet]
    [Route("Country")]
    public async Task<ActionResult<IEnumerable<Review>>> Country(string country)
    {
      var query = _db.Reviews.AsQueryable();

      if (country != null)
      {
        var id = _db.Destinations.Where(x => x.Country == country).ToList();
        List<int> www = new List<int> {};
        foreach (var thing in id) 
        {
          www.Add(thing.DestinationId);
        }
        query = query.Where(r => www.Contains(r.DestinationId));
      }
      return await query.ToListAsync();
    }

     //GET: api/Destinations/City
    [HttpGet]
    [Route("City")]
    public async Task<ActionResult<IEnumerable<Review>>> City(string city)
    {
      var query = _db.Reviews.AsQueryable();

      if (city != null)
      {
        var id = _db.Destinations.Where(x => x.City == city).ToList();
        List<int> www = new List<int> {};
        foreach (var thing in id) 
        {
          www.Add(thing.DestinationId);
        }
        query = query.Where(r => www.Contains(r.DestinationId));
      }
      return await query.ToListAsync();
    }

    // GET: api/Destinations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Destination>> GetDestination(int id)
    {
        var destination = await _db.Destinations.FindAsync(id);

        if (destination == null)
        {
            return NotFound();
        }

        return destination;
    }

    // PUT: api/Destinations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Destination destination)
    {
      if (id != destination.DestinationId)
      {
        return BadRequest();
      }

      _db.Entry(destination).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!DestinationExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Destinations
    [HttpPost]
    public async Task<ActionResult<Destination>> Post(Destination destination)
    {
      _db.Destinations.Add(destination);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetDestination), new { id = destination.DestinationId }, destination);
    }

    // DELETE: api/Destinations/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDestination(int id)
    {
      var destination = await _db.Destinations.FindAsync(id);
      if (destination == null)
      {
        return NotFound();
      }

      _db.Destinations.Remove(destination);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool DestinationExists(int id)
    {
      return _db.Destinations.Any(e => e.DestinationId == id);
    }
     //GET: api/Destinations/Random
    [HttpGet]
    [Route("Random")]
    public async Task<ActionResult<IEnumerable<Destination>>> Random()
    {
      var query = _db.Destinations.AsQueryable();
      Random random = new Random();
      int r = 0;
      while (!query.Any(d => d.DestinationId == r))
      {
        r = random.Next(1, (query.Count() + 1));
      }
      query = query.Where(entry => entry.DestinationId == r);
      return await query.ToListAsync();
    }
  }
}