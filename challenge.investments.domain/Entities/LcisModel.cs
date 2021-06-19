using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace challenge.investments.domain.Entities
{
	public class LCIsModel
	{
		[JsonPropertyName("lcis")]
		public List<Lci> Lcis { get; set; } = new List<Lci>();
	}

	public class Lci
	{
		[JsonPropertyName("capitalInvestido")]
		public decimal CapitalInvestido { get; set; }
		[JsonPropertyName("capitalAtual")]
		public decimal CapitalAtual { get; set; }
		[JsonPropertyName("quantidade")]
		public decimal Quantidade { get; set; }
		[JsonPropertyName("vencimento")]
		public DateTime Vencimento { get; set; }
		[JsonPropertyName("iof")]
		public decimal Iof { get; set; }
		[JsonPropertyName("outrasTaxas")]
		public decimal OutrasTaxas { get; set; }
		[JsonPropertyName("taxas")]
		public decimal Taxas { get; set; }
		[JsonPropertyName("indice")]
		public string Indice { get; set; }
		[JsonPropertyName("tipo")]
		public string Tipo { get; set; }
		[JsonPropertyName("nome")]
		public string Nome { get; set; }
		[JsonPropertyName("guarantidoFGC")]
		public bool GuarantidoFGC { get; set; }
		[JsonPropertyName("dataOperacao")]
		public DateTime DataOperacao { get; set; }
		[JsonPropertyName("precoUnitario")]
		public decimal PrecoUnitario { get; set; }
		[JsonPropertyName("primario")]
		public bool Primario { get; set; }
	}
}
