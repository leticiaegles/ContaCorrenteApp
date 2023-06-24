using ContaCorrentLibrary.Models;
using ContaCorrentLibrary.Models.Enums;
using ContaCorrentLibrary.Repositories;

namespace ContaCorrenteApi.Services;

public interface IMovementService : IRepository<MovementModel>
{
    Task<List<MovementModel>> ConsultarPeriodo(int id, DateTime inicio, DateTime fim);
    Task<List<MovementModel>> ConsultarOperacao(int id, TypeEnum tipoOperacao);
    Task RealizarTransferencia(int senderId, int recipientId, decimal amount);
}