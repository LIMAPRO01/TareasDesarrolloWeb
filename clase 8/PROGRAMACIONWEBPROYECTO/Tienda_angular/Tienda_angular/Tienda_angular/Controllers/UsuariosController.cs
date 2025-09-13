using System;
using CiberZone.Api.Data;
using CiberZone.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tienda_angular.Models;

namespace CiberZone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _db;
    public UsuariosController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        => Ok(await _db.Usuarios.AsNoTracking().ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Usuario>> GetById(int id)
    {
        var item = await _db.Usuarios.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> Create(Usuario dto)
    {
        _db.Usuarios.Add(dto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = dto.Id_Usuario }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Usuario dto)
    {
        if (dto.Id_Usuario != id) return BadRequest("ID mismatch");
        _db.Entry(dto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = await _db.Usuarios.FindAsync(id);
        if (item is null) return NotFound();
        _db.Usuarios.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
