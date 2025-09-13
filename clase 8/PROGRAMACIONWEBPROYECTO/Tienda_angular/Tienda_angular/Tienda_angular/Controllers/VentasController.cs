using System;
using CiberZone.Api.Data;
using CiberZone.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tienda_angular.Models;

namespace CiberZone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly AppDbContext _db;
    public VentasController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venta>>> GetAll()
        => Ok(await _db.Ventas.AsNoTracking().ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Venta>> GetById(int id)
    {
        var item = await _db.Ventas.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Venta>> Create(Venta dto)
    {
        _db.Ventas.Add(dto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = dto.Id_Venta }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Venta dto)
    {
        if (dto.Id_Venta != id) return BadRequest("ID mismatch");
        _db.Entry(dto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = await _db.Ventas.FindAsync(id);
        if (item is null) return NotFound();
        _db.Ventas.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

