using System.ComponentModel.DataAnnotations;

namespace EtecVeiculos.Api.Models;

public class TipoVeiculo
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    public string Nome { get; set; }
}
