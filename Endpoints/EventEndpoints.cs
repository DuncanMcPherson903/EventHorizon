using Microsoft.EntityFrameworkCore;
using EventHorizon.Data;
using EventHorizon.DTO;
using EventHorizon.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace EventHorizon.Endpoints;

public static class EventEndpoints
{
  public static void MapEventEndpoints(this WebApplication app)
  {
    app.MapGet("/events", async (EventHorizonDbContext dbContext, IMapper mapper, int? categoryId, DateTime? fromDate, DateTime? toDate) =>
    {
      var events = await dbContext.Events
        .Where((e) => categoryId == null ? (e.Id > 0) : (e.EventCategoryId == categoryId))
        .Where((e) => fromDate == null ? (e.Id > 0) : (e.DateTime > fromDate))
        .Where((e) => toDate == null ? (e.Id > 0) : (e.DateTime < toDate))
        .ProjectTo<EventDto>(mapper.ConfigurationProvider)
        .ToListAsync();

      return Results.Ok(events);
    });

    app.MapGet("/events/{id}", async (EventHorizonDbContext dbContext, IMapper mapper, int id) =>
    {
      var foundEvent = await dbContext.Events.FindAsync(id);

      if (foundEvent == null)
      {
        return Results.NotFound($"Event with ID of {id} not found");
      }

      return Results.Ok(mapper.Map<EventDetailDto>(foundEvent));
    });
  }
}
