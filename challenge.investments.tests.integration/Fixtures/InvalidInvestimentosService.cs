using System.Threading.Tasks;
using challenge.investments.domain.Entities;
using challenge.investments.domain.Interfaces.Services;

namespace challenge.investments.tests.integrations.Fixtures
{
    public class InvalidInvestimentosService : IInvestimentosService
    {
        public async Task<InvestimentosModel> Get()
        {
            return await Task.Run(() => new InvestimentosModel());
        }
    }
}
