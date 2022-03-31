using AutoMapper;
using LinqCrudTest.DTOs;
using LinqCrudTest.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinqCrudTest.Controllers;

[ApiController]
[Route("api/positions")]
public class PositionsController : ControllerBase
{
    private readonly AppDbContext context;
    private readonly IMapper mapper;

    public PositionsController(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<PositionDto>>> Get()
    {
        var positions = await context.Positions.ToListAsync();

        return mapper.Map<List<PositionDto>>(positions);
    }

    [HttpGet("{id:int}", Name = "getPositionById")]
    public async Task<ActionResult<PositionDto>> Get(int id)
    {
        var position = await context.Positions.FirstOrDefaultAsync(x => x.Id == id);

        if(position == null) return NotFound();

        return mapper.Map<PositionDto>(position);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreatePositionsDto createPositions)
    {
        var positionExist = await context.Positions.AnyAsync(x => x.Name == createPositions.Name);

        if(positionExist) return BadRequest("Position already created.");

        var position = mapper.Map<Positions>(createPositions);
        context.Add(position);
        await context.SaveChangesAsync();
        var positionDto = mapper.Map<PositionDto>(position);
        return new CreatedAtRouteResult("getPositionById", new {id = position.Id}, positionDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] CreatePositionsDto updatePosition)
    {
        var position = await context.Positions.FirstOrDefaultAsync(x => x.Id == id);

        if (position == null) return NotFound();

        position = mapper.Map(updatePosition, position);

        await context.SaveChangesAsync();
        var positionDto = mapper.Map<PositionDto>(position);
        return new CreatedAtRouteResult("getPositionById", new {id}, positionDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var position = await context.Positions.AnyAsync(x => x.Id == id);

        if(!position) return NotFound();
        
        context.Remove(new Positions {Id = id});
        await context.SaveChangesAsync();
        return NoContent();
    }
}
