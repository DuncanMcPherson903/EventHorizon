using Microsoft.EntityFrameworkCore;
using EventHorizon.Data;
// using EventHorizon.DTO;
using EventHorizon.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventHorizon.DTO;

namespace EventHorizon.Endpoints;

public static class CategoryEndpoints
{
  public static void MapCategoryEndpoints(this WebApplication app)
  {
    app.MapGet("/categories", async (EventHorizonDbContext dbContext, IMapper mapper) =>
    {
      var categories = await dbContext.EventCategories
        .ProjectTo<EventCategoryDto>(mapper.ConfigurationProvider)
        .ToListAsync();

      return Results.Ok(categories);
    });
  }
}
