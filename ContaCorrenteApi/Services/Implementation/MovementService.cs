using ContaCorrentLibrary.DataAcess;
using ContaCorrentLibrary.Models;
using ContaCorrentLibrary.Models.Enums;
using ContaCorrentLibrary.Repositories;
using Microsoft.Extensions.Configuration;

namespace ContaCorrenteApi.Services;

public class MovementService : Repository<MovementModel>, IMovementService
{
    public MovementService(ISqlDataAcess sql, IConfiguration config) : base(sql, config)
    {

    }

    public async Task<List<MovementModel>> ConsultarPeriodo(int id, DateTime inicial, DateTime final)
    {
        return await Obter(
            new
            {
                AccountId = id,
                InitialDate = $"{inicial:yyyy/MM/dd}",
                FinalDate = $"{final:yyyy/MM/dd}"
            },
            "dbo.spContaCorrente_ContaPeriodo");
    }

    public async Task<List<MovementModel>> ConsultarOperacao(int id, TypeEnum tipoOperacao)
    {
        return await Obter(
            new
            {
                AccountId = id,
                Type = tipoOperacao,
            },
            "dbo.spContaCorrente_ContaTipoMovimento");
    }

    public async Task RealizarTransferencia(int senderId, int recipientId, decimal amount)
    {
        for (int i = 0; i < 2; i++)
        {
            await Atualizar(new
            {
                AccountId = i == 0 ? senderId : recipientId,
                Description = i == 0 ? $"Enviado {amount} para {recipientId}" : $"Creditado R${amount} de {senderId}",
                TipoOperacao = i == 0 ? TypeEnum.Debit : TypeEnum.Credit,
                Amount = i == 0 ? decimal.Negate(amount) : amount,
            }, "dbo.spContaCorrente_Transferencia");
        }
    }
}