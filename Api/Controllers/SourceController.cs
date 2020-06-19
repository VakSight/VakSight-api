using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Sources;
using Services.Interfaceses;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("source")]
    public class SourceController : ControllerBase
    {
        private ISourceService sourceService;

        public SourceController(ISourceService sourceService)
        {
            this.sourceService = sourceService;
        }

        [HttpPost]
        [Route("api/electronic")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateElectronicSource(ElectronicSource source)
        {
            var content = await sourceService.CreateSourceAsync(source);
            return Ok(content);
        }
       
        [HttpPost]
        [Route("api/book")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateBookSource(BookSource source)
        {
            var content = await sourceService.CreateSourceAsync(source);
            return Ok(content);
        }

        [HttpPost]
        [Route("api/periodical")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePeriodicalSource(PeriodicalSource source)
        {
            var content = await sourceService.CreateSourceAsync(source);
            return Ok(content);
        }
    }
}
