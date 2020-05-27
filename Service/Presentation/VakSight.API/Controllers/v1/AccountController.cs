using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VakSight.API.Extensions;
using VakSight.API.Models;
using VakSight.Entities.Accounts;
using VakSight.Entities.Auth;
using VakSight.Service.Contracts;
using VakSight.Shared.Api.Controllers;
using VakSight.Shared.Models;

namespace VakSight.API.Controllers.v1
{
    [ApiController]
    [ApiVersionNeutral]
    [ApiRoute("account")]
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAccountsService accountService;
        private readonly IMapper mapper;

        public AccountController(IAccountsService accountService, IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(SuccessResultModel<LoginResult>), 200)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var data = mapper.Map<AccountLoginData>(model);
            return Ok(await accountService.LoginAsync(data));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(SuccessResultModel<BaseAccount>), 200)]
        public async Task<IActionResult> CreateAccount([FromBody] AccountModel model)
        {
            var newAccount = mapper.Map<CreateAccount>(model);
            var result = await accountService.CreateAccountAsync(CurrentUser, newAccount);
            return Ok(result);
        }
    }
}
