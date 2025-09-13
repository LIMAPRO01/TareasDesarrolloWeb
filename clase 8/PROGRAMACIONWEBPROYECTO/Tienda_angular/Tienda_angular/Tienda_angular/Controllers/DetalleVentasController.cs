using System;
using CiberZone.Api.Data;
using CiberZone.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tienda_angular.Models;

namespace CiberZone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalleVentasController : ControllerBase
{
    private readonly AppDbContext _db;
    public DetalleVentasController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalleVenta>>> GetAll()
        => Ok(await _db.Detalle_Ventas.AsNoTracking().ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DetalleVenta>> GetById(int id)
    {
        var item = await _db.Detalle_Ventas.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<DetalleVenta>> Create(DetalleVenta dto)
    {
        _db.Detalle_Ventas.Add(dto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = dto.Id_Detalle }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, DetalleVenta dto)
    {
        if (dto.Id_Detalle != id) return BadRequest("ID mismatch");
        _db.Entry(dto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = await _db.Detalle_Ventas.FindAsync(id);
        if (item is null) return NotFound();
        _db.Detalle_Ventas.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

