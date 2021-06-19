using System;
using System.Linq;
using System.Threading.Tasks;
using challenge.investments.domain.Entities;
using challenge.investments.domain.Interfaces.Repository;
using challenge.investments.domain.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace challenge.investments.service.Service
{
    public class InvestimentosService : IInvestimentosService
    {
        public readonly IFundosRepository _fundosRepository;
        public readonly ILcisRepository _rendaFixaRepository;
        public readonly ITesouroDiretoRepository _tesouroDiretoRepository;
        private readonly IMemoryCache _memoryCache;

        public InvestimentosService(IFundosRepository fundosRepository, ILcisRepository rendaFixaRepository, ITesouroDiretoRepository tesouroDiretoRepository, IMemoryCache memoryCache)
        {
            _fundosRepository = fundosRepository;
            _rendaFixaRepository = rendaFixaRepository;
            _tesouroDiretoRepository = tesouroDiretoRepository;
            _memoryCache = memoryCache;
        }
        public async Task<InvestimentosModel> Get()
        {
            if (_memoryCache.TryGetValue("investimentosData", out InvestimentosModel fundosModel))
                return fundosModel;


            var investimentos = new InvestimentosModel();

            //Calculo Valor total Investimentos
            var fundosResult = await _fundosRepository.Get();
            var lcisResult = await _rendaFixaRepository.Get();
            var tesouroResult = await _tesouroDiretoRepository.Get();

            if (fundosResult.Fundos == null && lcisResult.Lcis == null && tesouroResult.Tesouros == null)
                return new InvestimentosModel();

            var sumValorFundos = fundosResult.Fundos.Sum(x => x.ValorAtual);
            var sumValorLcis = lcisResult.Lcis.Sum(x => x.CapitalAtual);
            var sumValorTesouros = tesouroResult.Tesouros.Sum(x => x.ValorTotal);

            var valorTotal = sumValorFundos + sumValorLcis + sumValorTesouros;
            investimentos.ValorTotal = valorTotal;

            //Fundos investimentos
            fundosResult.Fundos.ForEach(x =>
            {
                var investimento = new Investimento
                {
                    Nome = x.Nome,
                    ValorInvestido = x.CapitalInvestido,
                    ValorTotal = x.ValorAtual,
                    Vencimento = x.DataResgate,
                    Ir = CalculoIR(x.ValorAtual, x.CapitalInvestido, TipoInvestimento.Fundo),
                    ValorResgate = CalculoResgate(x.DataResgate, x.DataCompra, x.ValorAtual),
                };
                investimentos.Investimentos.Add(investimento);
            });

            //Lcis investimentos
            lcisResult.Lcis.ForEach(x =>
            {
                var investimento = new Investimento
                {
                    Nome = x.Nome,
                    ValorInvestido = x.CapitalInvestido,
                    ValorTotal = x.CapitalAtual,
                    Vencimento = x.Vencimento,
                    Ir = CalculoIR(x.CapitalAtual, x.CapitalInvestido, TipoInvestimento.Fundo),
                    ValorResgate = CalculoResgate(x.Vencimento, x.DataOperacao, x.CapitalAtual),
                };
                investimentos.Investimentos.Add(investimento);
            });


            //Tesouro investimentos
            tesouroResult.Tesouros.ForEach(x =>
            {
                var investimento = new Investimento
                {
                    Nome = x.Nome,
                    ValorInvestido = x.ValorInvestido,
                    ValorTotal = x.ValorTotal,
                    Vencimento = x.Vencimento,
                    Ir = CalculoIR(x.ValorTotal, x.ValorInvestido, TipoInvestimento.Fundo),
                    ValorResgate = CalculoResgate(x.Vencimento, x.DataDeCompra, x.ValorTotal),
                };
                investimentos.Investimentos.Add(investimento);
            });

            return _memoryCache.GetOrCreate("investimentosData", e =>
            {
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return investimentos;
            });
        }

        private decimal CalculoIR(decimal valorTotal, decimal valorInvestido, TipoInvestimento tipoInvestimento)
        {
            var rentabilidade = valorTotal - valorInvestido;

            var ir = tipoInvestimento switch
            {
                TipoInvestimento.Fundo => rentabilidade * (15 / 100m),
                TipoInvestimento.TesouroDireto => rentabilidade * (10 / 100m),
                _ => rentabilidade * (5 / 100m),
            };
            return ir;
        }

        private decimal CalculoResgate(DateTime dataVencimento, DateTime dataCompra, decimal valorTotal)
        {
            var todayDate = DateTime.Now;

            var mesDiffFinal = AjustaMesAno(dataVencimento) - AjustaMesAno(dataCompra);
            if (dataVencimento.Day > dataCompra.Day)
                mesDiffFinal--;

            var mesDiffHoje = AjustaMesAno(todayDate) - AjustaMesAno(dataCompra);
            if (todayDate.Day > dataCompra.Day)
                mesDiffHoje--;

            var mesDiff = mesDiffFinal - mesDiffHoje;

            if (mesDiff <= 3)
                return valorTotal - (valorTotal * (6 / 100m));
            else if (mesDiffHoje > (mesDiffFinal / 2))
                return valorTotal - (valorTotal * (15 / 100m));
            else
                return valorTotal - (valorTotal * (30 / 100m));
        }

        public int AjustaMesAno(DateTime d)
        {
            return d.Year * 12 + d.Month;
        }
    }
}
