using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TodoApp_API.Models;
using TodoApp_API.Services;


namespace TodoApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        //Constructor of Account Controller getting its specific collection from mongoDBService
        public AccountController(MongoDBService mongoDBService) {
            mongoDBService.GetAccountCollection();
            _accountService = mongoDBService._accountService;
        }

        //Getting info of all existing Accounts
        [HttpGet, Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<Account>> GetAllAccounts() =>
            await _accountService.GetAsync();


        //Searching accounts by email and password for login purpose or other..
        [HttpGet("login/{email}/{password}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Account>> GetAccountByEmail([FromRoute(Name = "email")] string email, [FromRoute(Name = "password")] string password)
        {
            //checking if exists account with entered email
            var account = await _accountService.GetAsync(email, password);
            if (account is null)
            {
                return NotFound();//not exist returning not found..
            }
            return account;//found and returning account info
        }

        //Creating new Account by signup
        [HttpPost, Route("signup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNewAccount(Account newAccount)
        {
            await _accountService.CreateAsync(newAccount);
            return CreatedAtAction(nameof(CreateNewAccount), new { id = newAccount._Id }, newAccount);
        }

        //updating specific account by account _id giving from client
        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateExistingAccount(string id, Account updatedAccount)
        {
            //checking if such account with id exists
            var account = await _accountService.GetAsync(id);
            if (account is null)
            {
                return NotFound();//not exist returning not found..
            }
            updatedAccount._Id = account._Id;
            await _accountService.UpdateAsync(id, updatedAccount);//found and giving update info
            return NoContent();
        }

        //Deleting specific account with corresponding id
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteExistingAccount(string id)
        {
            //checking if account exist
            var account = await _accountService.GetAsync(id);

            if (account is null)
            {
                return NotFound();//not exist returning not found..
            }

            await _accountService.RemoveAsync(id);//removing user from database

            return NoContent();
        }
    }
}