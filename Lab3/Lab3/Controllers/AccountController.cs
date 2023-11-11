using Lab3.Models;
using Lab3.Services;
using Lab3.Services.Interfaces.Services;
using Lab3.Services.Interfaces.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Lab3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController:Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService) 
        {            
            this.accountService = accountService;
        }

        //GET: /account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return new ObjectResult(await accountService.GetAllAsync());
        }

        //GET: /account/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Account>> GetAccountById(int id)
        {
            Account result;
            try
            {
                result = await accountService.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            return new ObjectResult(result);
        }
        //DELETE: /account/id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Account>> DeleteAccountById(int id)
        {
            Account result;
            try
            {
                result = await accountService.DeleteAsync(id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            
            return new ObjectResult(result);
        }
        // POST: /account
        [HttpPost]
        public async Task<ActionResult<Account>> AddAccount([FromBody] Account account)
        {
            try
            {
                await accountService.AddAsync(account);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            return new OkObjectResult(account);
        }
        // POST: /account/topup
        [HttpPost("topup")]
        public async Task<ActionResult<Account>> TopUpBalance([FromBody] TopUpBalanceModel model)
        {
            try
            {
                await accountService.TopUpBalance(model.AccountId, model.Amount);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            return new OkObjectResult(model);
        }
    }
}
