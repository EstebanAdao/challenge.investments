using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace challenge.investments.domain.Entities
{
    public class TesourosModel
    {
        [JsonPropertyName("tds")]
        public List<Tesouro> Tesouros { get; set; } = new List<Tesouro>();
    }

    public class Tesouro
    {
        [JsonPropertyName("valorInvestido")]
        public decimal ValorInvestido { get; set; }
        [JsonPropertyName("valorTotal")]
        public decimal ValorTotal { get; set; }
        [JsonPropertyName("vencimento")]
        public DateTime Vencimento { get; set; }
        [JsonPropertyName("dataDeCompra")]
        public DateTime DataDeCompra { get; set; }
        [JsonPropertyName("iof")]
        public decimal Iof { get; set; }
        [JsonPropertyName("indice")]
        public string Indice { get; set; }
        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }

}
