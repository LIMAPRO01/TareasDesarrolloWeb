using System;
using CiberZone.Api.Data;
using CiberZone.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tienda_angular.Models;

namespace CiberZone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _db;
    public CategoriasController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
        => Ok(await _db.Categorias.AsNoTracking().ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Categoria>> GetById(int id)
    {
        var item = await _db.Categorias.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> Create(Categoria dto)
    {
        _db.Categorias.Add(dto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = dto.Id_Categoria }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Categoria dto)
    {
        if (dto.Id_Categoria != id) return BadRequest("ID mismatch");
        _db.Entry(dto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = await _db.Categorias.FindAsync(id);
        if (item is null) return NotFound();
        _db.Categorias.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
