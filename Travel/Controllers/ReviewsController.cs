using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel.Models;
using Travel.Wrappers;
// using Travel.Contexts;
using Travel.Filter;
using Travel.Helpers;
using Travel.Services;

namespace Travel.Controllers
{
  #pragma warning disable CS1591
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private readonly TravelContext _db;
    private readonly IUriService uriService;

    public ReviewsController(TravelContext db, IUriService uriService)
    {
      this.uriService = uriService;
      _db = db;
    }

    //GET: api/Reviews
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Review>>> Get(int destinationId, double rating)
    // {
    //   var query = _db.Reviews.AsQueryable();

    //   if (rating > 0)
    //   {
    //     query = query.Where(entry => entry.Rating >= rating);
    //   }
    //   return await query.ToListAsync();
    // }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
    {
      var route = Request.Path.Value;
      var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
      var pagedData = await _db.Reviews
        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
        .Take(validFilter.PageSize)
        .ToListAsync();
      var totalRecords = await _db.Reviews.CountAsync();
      var pagedResponse = PaginationHelper.CreatePagedResponse<Review>(pagedData, validFilter, totalRecords, uriService, route);
      return Ok(pagedResponse);
    }

     //GET: api/Reviews/Popular
    [HttpGet]
    [Route("Popular")]
    public async Task<ActionResult<IEnumerable<Destination>>> Popular()
    {
      var query = _db.Destinations.AsQueryable();

      var all = _db.Reviews.GroupBy(x => x.DestinationId)
        .Select(group => new {DestinationId = group.Key, Count = group.Count()})
        .OrderByDescending(x => x.Count);

      var item = all.First();
      int mostfrequent = item.DestinationId;
      var mostfrequentcount = item.Count;

      query = query.Where(entry => entry.DestinationId == mostfrequent);
      return await query.ToListAsync();
    }

    // GET: api/Reviews/5
    // [HttpGet("{id}")]
    // public async Task<ActionResult<Review>> GetReview(int id, int destinationId, double rating)
    // {
        
    //     var review = await _db.Reviews.FindAsync(id);

    //     if (destinationId == 0)
    //     {
          
    //     }

    //     if (review == null)
    //     {
    //         return NotFound();
    //     }
    //     return review;
    // }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var review = await _db.Reviews.Where(a => a.ReviewId == id).FirstOrDefaultAsync();
      return Ok(new Response<Review>(review));
    }


    // PUT: api/Reviews/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Review review)
    {
      if (id != review.ReviewId)
      {
        return BadRequest();
      }

      _db.Entry(review).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ReviewExists(id))
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

    // POST: api/Reviews
    [HttpPost]
    public async Task<ActionResult<Review>> Post(Review review)
    {
      _db.Reviews.Add(review);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetById), new { id = review.ReviewId }, review);
    }

    // DELETE: api/Reviews/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
      var review = await _db.Reviews.FindAsync(id);
      if (review == null)
      {
        return NotFound();
      }

      _db.Reviews.Remove(review);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool ReviewExists(int id)
    {
      return _db.Reviews.Any(e => e.ReviewId == id);
    }
  }
  #pragma warning restore CS1591
}