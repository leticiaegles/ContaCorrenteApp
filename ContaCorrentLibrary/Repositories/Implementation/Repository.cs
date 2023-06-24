using ContaCorrentLibrary.DataAcess;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ContaCorrentLibrary.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly string connectionString;
    private readonly ISqlDataAcess _sql;

    public Repository(ISqlDataAcess sql, IConfiguration config)
    {
        this._sql = sql;
        connectionString = config.GetConnectionString("Default")!;
    }

    public async Task<T> Criar<U>(U parameters, string storedProcedure)
    {
        using IDbConnection connection = new SqlConnection(connectionString);

        var result = await connection.QueryAsync<T>(
            sql: storedProcedure,
            param: parameters,
            commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault()!;
    }

    public async Task<List<T>> Obter<U>(U parameters, string storedProcedure)
    {
        using IDbConnection connection = new SqlConnection(connectionString);

        var result = await connection.QueryAsync<T>(
            sql: storedProcedure,
            param: parameters,
            commandType: CommandType.StoredProcedure);

        return result.ToList();
    }

    public async Task Atualizar<U>(U parameters, string storedProcedure)
    {
        using IDbConnection connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }
}
