using EtecVeiculos.Api.Data;
using EtecVeiculos.Api.DTOs;
using EtecVeiculos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtecVeiculos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Marcas : ControllerBase
{
    private readonly AppDbContext _context;

    public Marcas(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Marca>>> Get()
    {
        var tipos = await _context.Marcas.ToListAsync();
        return Ok(tipos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Marca>> Get(int id)
    {
        var tipo = await _context.Marcas.FindAsync(id);
        if (tipo == null)
            return NotFound("Marca não encontrada");
        return Ok(tipo);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(MarcaDto marcaDto)
    {
        if (ModelState.IsValid)
        {
            Marca marca = new()
            {
                Nome = marcaDto.Nome
            };
            await _context.AddAsync(marca);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = marca.Id });
        }
        return BadRequest("Verifique os dados informados");
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Edit(int id, Marca marca)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (!_context.Marcas.Any(q => q.Id == id))
                    return NotFound("Marca não encontrada");
                
                if (id != marca.Id)
                    return BadRequest("Verifique os dados informados");
                
                _context.Entry(marca).State = EntityState.Modified;
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
            var marca = await _context.Marcas
                .FirstOrDefaultAsync(q => q.Id == id);
            if (marca == null)
                return NotFound("Marca não Encontrada");
            
            _context.Remove(marca);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um problema: {ex.Message}");
        }
    }

}
