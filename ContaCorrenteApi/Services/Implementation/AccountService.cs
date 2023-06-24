using ContaCorrentLibrary.DataAcess;
using ContaCorrentLibrary.Models;
using ContaCorrentLibrary.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using System;

namespace ContaCorrenteApi.Services;

public class AccountService : Repository<AccountModel>, IAccountService
{
    public AccountService(ISqlDataAcess sql, IConfiguration config) : base(sql, config)
    {

    }

    public async Task<AccountModel> CriarContaCorrente(string personName)
    {
        return await Criar(new { Name = personName }, "dbo.spContaCorrente_Create");
    }

    public async Task<AccountModel?> ConsultarConta(int id)
    {
        var contaCorrente = await Obter(new { Id = id }, "dbo.spContaCorrente_Conta");

        return contaCorrente.FirstOrDefault();
    }

    public async Task<decimal> ConsultarSaldo(int id)
    {
        var contaCorrente = await Obter(new { Id = id }, "dbo.spContaCorrente_Conta");

        return contaCorrente.FirstOrDefault().Balance;
    }

    public async Task<decimal> ConsultarSaldoPeriodo(int id, DateTime inicio, DateTime fim)
    {
        //TODO
        var contaCorrente = await Obter(new { Id = id }, "dbo.spContaCorrente_Saldo");

        return contaCorrente.FirstOrDefault().Balance;
    }

    public async Task RealizarDeposito(int id, decimal amount)
    {
        var contaCorrente = (await ConsultarConta(id)).Balance;

        var newBalance = contaCorrente + amount;

        await Atualizar(new { Id = id , Balance = newBalance}, "dbo.spContaCorrente_Deposito");
    }

    public async Task<string> RealizarSaque(int id, decimal amount)
    {
        var contaCorrente = await ConsultarConta(id);

        if (amount > contaCorrente.Balance || amount > contaCorrente.Limit)
            return "Saldo Insuficiente!";

        var newBalance = contaCorrente.Balance - amount;

        await Atualizar(new { Id = id, Amount = newBalance }, "dbo.spContaCorrente_Saque");

        return "Saque Realizado!";
    }
}