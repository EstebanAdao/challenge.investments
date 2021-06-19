using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace challenge.investments.domain.Entities
{
    public class FundosModel
    {
        [JsonPropertyName("fundos")]
        public List<Fundo> Fundos { get; set; } = new List<Fundo>();
    }

    public class Fundo
    {
        [JsonPropertyName("capitalInvestido")]
        public decimal CapitalInvestido { get; set; }
        [JsonPropertyName("ValorAtual")]
        public decimal ValorAtual { get; set; }
        [JsonPropertyName("dataResgate")]
        public DateTime DataResgate { get; set; }
        [JsonPropertyName("dataCompra")]
        public DateTime DataCompra { get; set; }
        [JsonPropertyName("iof")]
        public decimal Iof { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("totalTaxas")]
        public decimal TotalTaxas { get; set; }
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
    }
}
