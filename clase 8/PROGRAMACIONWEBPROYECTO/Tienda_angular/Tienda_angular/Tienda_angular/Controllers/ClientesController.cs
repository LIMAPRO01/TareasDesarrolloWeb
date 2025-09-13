using System;
using CiberZone.Api.Data;
using CiberZone.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tienda_angular.Models;

namespace CiberZone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _db;
    public ClientesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        => Ok(await _db.Clientes.AsNoTracking().ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Cliente>> GetById(int id)
    {
        var item = await _db.Clientes.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> Create(Cliente dto)
    {
        _db.Clientes.Add(dto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = dto.Id_Cliente }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Cliente dto)
    {
        if (dto.Id_Cliente != id) return BadRequest("ID mismatch");
        _db.Entry(dto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = await _db.Clientes.FindAsync(id);
        if (item is null) return NotFound();
        _db.Clientes.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

