using Dapper;
using Microsoft.Extensions.Configuration;
using Repository.Models;
using System.Data.SqlClient;

namespace Repository
{
    public class QuoteRepository : IQuoteRepository
    {
        // Make it possible to read a connection string from configuration
        private readonly IConfiguration configuration;

        public QuoteRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(Quote entity)
        {
            var sql = "Insert into Quote (Name) VALUES (@Name)";

            // Sing the Dapper Connection string we open a connection to the database
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                // Pass the product object and the SQL statement into the Execute function (async)
                return await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = "DELETE FROM Quote WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Quote>> GetAllAsync()
        {
            var sql = "SELECT * FROM Quote";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                // Map all products from database to a list of type Product defined in Models.
                // this is done by using Async method which is also used on the GetByIdAsync
                var result = await connection.QueryAsync<Quote>(sql);
                return result.ToList();
            }
        }

        public async Task<Quote> GetByIdAsync(long id)
        {
            var sql = @"SELECT * FROM Quote WHERE Id = @Id 
                        SELECT * FROM Employee WHERE QuoteId = @Id
                        SELECT * FROM Dependent WHERE EmployeeId IN (SELECT Id FROM Employee WHERE QuoteId = @Id)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var multi = await connection.QueryMultipleAsync(sql, new { Id = id }))
                {
                    var quote = multi.Read<Quote>().Single();
                    quote.Employees = multi.Read<Employee>().ToList();
                    var dependents = multi.Read<Dependent>().ToList();
                    foreach (var employee in quote.Employees)
                    {
                        employee.Dependents = dependents.Where(x => x.EmployeeId == employee.Id).ToList();
                    }
                    return quote;
                }
            }
        }

        public async Task<int> UpdateAsync(Quote entity)
        {
            var sql = "UPDATE Quote SET Name = @Name WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = entity.Id, Name = entity.Name } );
                return result;
            }
        }
    }
}
