using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_apis_web_services_fuel_manager.Models
{
    [Table("Consumos")]
    public class Consumo : LinksHATEOS
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
        [Required]
        public TipoCombustivel Tipo { get; set; }
        [Required]
        public int VeiculoId { get; set; } // relacionamento entre consumo e veiculos (ForeignKey)
        public Veiculo Veiculo { get; set; } // navegação virtual, ao carregar a informação do consumo, irá carregar todas as informações do veiculo relacionado a esse consumo
    }

    public enum TipoCombustivel 
    {
        Diesel,
        Etanol,
        Gasolina
    }
}
