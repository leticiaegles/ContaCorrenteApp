using ContaCorrentLibrary.Models;
using ContaCorrentLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContaCorrenteApi.Services;

public interface IAccountService : IRepository<AccountModel>
{
    Task<AccountModel> CriarContaCorrente(string personName);
    Task<AccountModel?> ConsultarConta(int id);
    Task<decimal> ConsultarSaldo(int id);
    Task<decimal> ConsultarSaldoPeriodo(int id, DateTime inicio, DateTime fim);
    Task RealizarDeposito(int id, decimal amount);
    Task<string> RealizarSaque(int id, decimal amount);



}