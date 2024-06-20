using System.ComponentModel.DataAnnotations;

namespace EtecVeiculos.Api.DTOs;

public class ModeloDto
{
    [Required]
    [StringLength(50)]
    public string Nome { get; set; }
}
