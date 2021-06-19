using System;
using System.Net;
using System.Threading.Tasks;
using challenge.investments.domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace challenge.investments.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestimentosController : ControllerBase
    {
        private readonly IInvestimentosService _investimentosService;

        public InvestimentosController(IInvestimentosService investimentosService)
        {
            _investimentosService = investimentosService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _investimentosService.Get();
                if (result.Investimentos.Count > 0)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
