using System;
using CiberZone.Api.Data;
using CiberZone.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tienda_angular.Models;

namespace CiberZone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProductosController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetAll()
        => Ok(await _db.Productos.AsNoTracking().ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Producto>> GetById(int id)
    {
        var item = await _db.Productos.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Producto>> Create(Producto dto)
    {
        _db.Productos.Add(dto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = dto.Id_Producto }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Producto dto)
    {
        if (dto.Id_Producto != id) return BadRequest("ID mismatch");
        _db.Entry(dto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = await _db.Productos.FindAsync(id);
        if (item is null) return NotFound();
        _db.Productos.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

