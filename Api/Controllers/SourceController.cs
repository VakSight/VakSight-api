using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Sources;
using Services.Interfaceses;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/source")]
    public class SourceController : ControllerBase
    {
        private ISourceService sourceService;

        public SourceController(ISourceService sourceService)
        {
            this.sourceService = sourceService;
        }

        [HttpPost]
        [Route("electronic")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateElectronicSource(ElectronicSource source)
        {
            var content = await sourceService.CreateSourceAsync(source);
            return Ok(content);
        }
       
        [HttpPost]
        [Route("book")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateBookSource(BookSource source)
        {
            var content = await sourceService.CreateSourceAsync(source);
            return Ok(content);
        }

        [HttpPost]
        [Route("periodical")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePeriodicalSource(PeriodicalSource source)
        {
            var content = await sourceService.CreateSourceAsync(source);
            return Ok(content);
        }

        [HttpPost]
        [Route("dissertation")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateDissertationSource(DissertationSource source)
        {
            var content = await sourceService.CreateSourceAsync(source);
            return Ok(content);
        }

        [HttpPost]
        [Route("abstractDissertation")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAbstractDissertationSource(AbstractDissertationSource source)
        {
            var content = await sourceService.CreateSourceAsync(source);
            return Ok(content);
        }
    }
}
