using System.ComponentModel.DataAnnotations;

namespace EtecVeiculos.Api.DTOs;

public class MarcaDto
{
    [Required]
    [StringLength(30)]
    public string Nome { get; set; }
}
