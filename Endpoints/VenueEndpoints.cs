using Microsoft.EntityFrameworkCore;
using EventHorizon.Data;
using EventHorizon.DTO;
using EventHorizon.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace EventHorizon.Endpoints;

public static class VenueEndpoints
{
  public static void MapVenueEndpoints(this WebApplication app)
  {
    app.MapGet("/venues", async (EventHorizonDbContext dbContext, IMapper mapper) =>
    {
      var venues = await dbContext.Venues
        .Where(v => v.IsActive)
        .ProjectTo<VenueDto>(mapper.ConfigurationProvider)
        .ToListAsync();

      return Results.Ok(venues);
    });

    app.MapGet("/venues/{id}", async (EventHorizonDbContext dbContext, IMapper mapper, int id) =>
    {
      var venue = await dbContext.Venues.FindAsync(id);

      if (venue == null)
      {
        return Results.NotFound($"Venue with ID of {id} not found");
      }

      return Results.Ok(mapper.Map<VenueDto>(venue));
    });

    app.MapPost("/venues", async (
      Venue newVenue,
      EventHorizonDbContext dbContext,
      IMapper mapper) =>
    {
      dbContext.Venues.Add(newVenue);
      await dbContext.SaveChangesAsync();

      return Results.Created();
    });

    app.MapPut("/venues/{id}", async (
      int id,
      VenueUpdateDto updateVenueDto,
      EventHorizonDbContext dbContext,
      IMapper mapper) =>
    {
      var venue = await dbContext.Venues.FindAsync(id);

      if (venue == null)
      {
        return Results.NotFound();
      }

      // Map from DTO to entity, updating only non-null properties
      mapper.Map(updateVenueDto, venue);

      await dbContext.SaveChangesAsync();

      return Results.Ok(mapper.Map<VenueDto>(venue));
    });

    app.MapDelete("/venues/{id}", async (
    int id,
    EventHorizonDbContext dbContext,
    IMapper mapper) =>
    {
      var venue = await dbContext.Venues.FindAsync(id);

      if (venue == null)
      {
        return Results.NotFound();
      }

      // Check if the venue has any events
      var hasEvent = await dbContext.Events
        .AnyAsync(e => e.VenueId == id);

      if (hasEvent)
      {
        return Results.Conflict("Venue currently has associated events");
      }

      // If no events reference this venue, we can safely delete it
      dbContext.Venues.Remove(venue);
      await dbContext.SaveChangesAsync();

      return Results.NoContent();
    });
  }
}
