namespace ContaCorrentLibrary.Repositories;

public interface IRepository<T>
{
    Task<T> Criar<U>(U parameters, string storedProcedure);
    Task<List<T>> Obter<U>(U parameters, string storedProcedure);
    Task Atualizar<U>(U parameters, string storedProcedure);
}
