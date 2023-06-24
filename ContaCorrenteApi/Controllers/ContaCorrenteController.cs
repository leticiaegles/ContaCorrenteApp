using Microsoft.AspNetCore.Mvc;
using ContaCorrentLibrary.Models;
using System;
using ContaCorrenteApi.Services;
using ContaCorrentLibrary.Models.Enums;

namespace ContaCorrenteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    { 
        private readonly IAccountService _accountService;
        private readonly IMovementService _movementService;
        public ContaCorrenteController(IAccountService accountService, IMovementService movementService)
        {
                _accountService = accountService;
            _movementService = movementService;

        }
        [HttpPost]
        public async Task<ActionResult<AccountModel>> CriarContaCorrente([FromBody] string name)
        {
            var response = await _accountService.CriarContaCorrente(name);
            return response;
        }

        [HttpPut("realizarDeposito/{id}/{amount}")]
        public async Task RealizarDeposito(int id, decimal amount)
        {
            await _accountService.RealizarDeposito(id, amount);
        }

        [HttpPut("realizarSaque/{id}/{amount}")]
        public async Task RealizarSaque(int id, decimal amount)
        {
            await _accountService.RealizarSaque(id, amount);
        }

        [HttpPut("realizarTransferencia/{idPortador}/{idDestino}/{amount}")]
        public async Task RealizarTransferencia(int senderId, int recipientId, decimal amount)
        {
            await _movementService.RealizarTransferencia(senderId, recipientId, amount);
        }

        [HttpGet("consultarSaldo/{id}")]
        public async Task<ActionResult<decimal>> ConsultarSaldo(int id)
        {
           var saldo =  await _accountService.ConsultarSaldo(id);
            return saldo;
        }

        [HttpGet("consultarExtratoPeriodo/{accountId}")]
        public async Task<ActionResult<IEnumerable<MovementModel>>> ConsultarExtratoPeriodo(int accountId, DateTime inicial, DateTime final)
        {
            var movements = await _movementService.ConsultarPeriodo(accountId, inicial, final);
            return movements;
        }

        [HttpGet("consultarExtratoTipoOperacao/{accountId}")]
        public async Task<ActionResult<IEnumerable<MovementModel>>> ConsultarExtratoTipoOperacao(int accountId, TypeEnum type)
        {
            var movements = await _movementService.ConsultarOperacao(accountId, type);
            return movements;
        }
    }
}
