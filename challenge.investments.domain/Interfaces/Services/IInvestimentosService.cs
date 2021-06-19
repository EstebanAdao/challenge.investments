using System.Threading.Tasks;
using challenge.investments.domain.Entities;

namespace challenge.investments.domain.Interfaces.Services
{
    public interface IInvestimentosService
    {
        Task<InvestimentosModel> Get();
    }
}
