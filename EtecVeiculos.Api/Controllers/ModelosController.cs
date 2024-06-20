using EtecVeiculos.Api.Data;
using EtecVeiculos.Api.DTOs;
using EtecVeiculos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtecVeiculos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Modelos : ControllerBase
{
    private readonly AppDbContext _context;

    public Modelos(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Modelo>>> Get()
    {
        var tipos = await _context.Modelos.Include(m => m.Marca).ToListAsync();
        return Ok(tipos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Modelo>> Get(int id)
    {
        var tipo = await _context.Modelos.Include(m => m.Marca).FirstOrDefaultAsync(m => m.Id == id);
        if (tipo == null)
            return NotFound("Modelo não encontrado");
        return Ok(tipo);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(ModeloDto modeloDto)
    {
        if (ModelState.IsValid)
        {
            Modelo modelo = new()
            {
                Nome = modeloDto.Nome
            };
            await _context.AddAsync(modelo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = modelo.Id });
        }
        return BadRequest("Verifique os dados informados");
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Edit(int id, Modelo modelo)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (!_context.Modelos.Any(q => q.Id == id))
                    return NotFound("Modelo não encontrado");
                
                if (id != modelo.Id)
                    return BadRequest("Verifique os dados informados");
                
                _context.Entry(modelo).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um problema: {ex.Message}");
            }
        }
        return BadRequest("Verifique os dados informados");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var modelo = await _context.Modelos
                .FirstOrDefaultAsync(q => q.Id == id);
            if (modelo == null)
                return NotFound("Modelo não Encontrado");
            
            _context.Remove(modelo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um problema: {ex.Message}");
        }
    }

}
