using System;
using System.Collections.Generic;

namespace challenge.investments.domain.Entities
{
    public class InvestimentosModel
    {
        public decimal ValorTotal { get; set; }
        public List<Investimento> Investimentos { get; set; } = new List<Investimento>();
    }

    public class Investimento
    {
        public string Nome { get; set; }
        public decimal ValorInvestido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Ir { get; set; }
        public decimal ValorResgate { get; set; }
    }
}
