using EtecVeiculos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EtecVeiculos.Api.Data;

public class AppDbSeed
{
    public AppDbSeed(ModelBuilder modelBuilder)
    {
        #region TipoVeiculo
        List<TipoVeiculo> tipoVeiculos = [
            new() {
                Id = 1,
                Nome = "Moto"
            },
            new() {
                Id = 2,
                Nome = "Carro"
            },
            new() {
                Id = 3,
                Nome = "Caminhão"
            }
        ];
        modelBuilder.Entity<TipoVeiculo>().HasData(tipoVeiculos);
        #endregion      

        #region Marcas
        List<Marca> marcas = new() {
            new() {
                Id = 1,
                Nome = "Honda"
            },
            new() {
                Id = 2,
                Nome = "Mitsubishi"
            },
            new() {
                Id = 3,
                Nome = "Nissan"
            }
        };
        modelBuilder.Entity<Marca>().HasData(marcas);
        #endregion

        #region Modelo
        // 2 Modelos por marca
        List<Modelo> modelos = new() {
            // Honda
            new() {
                Id = 1,
                Nome = "Civic",
                MarcaId = 1
            },
            new() {
                Id = 2,
                Nome = "Accord",
                MarcaId = 1
            },

            // Mitsubishi
            new() {
                Id = 3,
                Nome = "Triton",
                MarcaId = 2
            },
            new() {
                Id = 4,
                Nome = "Eclipse",
                MarcaId = 2
            },

            // NissaN
            new() {
                Id = 5,
                Nome = "Sentra",
                MarcaId = 3
            },
            new() {
                Id = 6,
                Nome = "Frontier",
                MarcaId = 3
            }

        };
        modelBuilder.Entity<Modelo>().HasData(modelos);
        #endregion  
    }
}
