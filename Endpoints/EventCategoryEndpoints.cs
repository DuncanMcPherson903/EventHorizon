using Microsoft.EntityFrameworkCore;
using EventHorizon.Data;
using EventHorizon.DTO;
using EventHorizon.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace EventHorizon.Endpoints;

public static class CategoryEndpoints
{
  public static void MapEventCategoryEndpoints(this WebApplication app)
  {
    app.MapGet("/categories", async (EventHorizonDbContext dbContext, IMapper mapper) =>
    {
      var categories = await dbContext.EventCategories
        .ProjectTo<EventCategoryDto>(mapper.ConfigurationProvider)
        .ToListAsync();

      return Results.Ok(categories);
    });

    app.MapGet("/categories/{id}", async (EventHorizonDbContext dbContext, IMapper mapper, int id) =>
    {
      var category = await dbContext.EventCategories.FindAsync(id);

      if (category == null)
      {
        return Results.NotFound($"Category with ID of {id} not found");
      }

      return Results.Ok(mapper.Map<EventCategoryDto>(category));
    });

    app.MapPost("/categories", async (
      EventCategory newCategory,
      EventHorizonDbContext dbContext,
      IMapper mapper) =>
    {
      dbContext.EventCategories.Add(newCategory);
      await dbContext.SaveChangesAsync();

      return Results.Created($"/categories/{newCategory.Name}", new { name = newCategory.Name });
    });

    app.MapPut("/categories/{id}", async (
      int id,
      EventCategoryDto eventCategoryDto,
      EventHorizonDbContext dbContext,
      IMapper mapper) =>
    {
      var category = await dbContext.EventCategories.FindAsync(id);

      if (category == null)
      {
        return Results.NotFound();
      }

      // Map from DTO to entity, requires all values in Dto to be sent
      mapper.Map(eventCategoryDto, category);

      await dbContext.SaveChangesAsync();

      return Results.Ok(mapper.Map<EventCategoryDto>(category));
    });

    app.MapDelete("/categories/{id}", async (
    int id,
    EventHorizonDbContext dbContext,
    IMapper mapper) =>
    {
      var category = await dbContext.EventCategories.FindAsync(id);

      if (category == null)
      {
        return Results.NotFound();
      }

      // Check if the category has any events
      var hasEvent = await dbContext.Events
        .AnyAsync(e => e.EventCategoryId == id);

      if (hasEvent)
      {
        return Results.Conflict("Category currently has associated events");
      }

      // If no events reference this category, we can safely delete it
      dbContext.EventCategories.Remove(category);
      await dbContext.SaveChangesAsync();

      return Results.NoContent();
    });
  }
}
