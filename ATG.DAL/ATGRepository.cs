using ATG.DAL.DatabaseModels;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ATG.DAL
{
    public class ATGRepository : IATGRepository
    {
        private readonly IConfiguration config;

        public ATGRepository(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(config.GetConnectionString("ATGConnectionString"));
            }
        }

        public async Task<int> CreateNewVisitor(ATGVisitor visitor)
        {
            using (var connection = Connection)
            {
                connection.Open();
                string sQuery = $"INSERT INTO dbo.ATGVisitor VALUES (@ip, @os, @browser, @sexID); SELECT CAST(SCOPE_IDENTITY() AS int)";
                var result = await connection.QueryAsync<int>(sQuery, new { ip = visitor.IPAddress, os = visitor.OS, browser = visitor.Browser, sexID = visitor.SexID});
                return result.Single();
            };
             
        }

        public async Task<List<ATGVisitor>> GetAllVisitors()
        {
            using (var connection = Connection)
            {
                connection.Open();
                string sQuery = "SELECT * FROM dbo.ATGVisitor";
                var result = await connection.QueryAsync<ATGVisitor>(sQuery);
                return result.ToList();
            };
        }

        public async Task<bool> UpdateByID(ATGVisitor visitor)
        {
            using (var connection = Connection)
            {
                connection.Open();
                string sQuery = "UPDATE dbo.ATGVisitor SET SexID = @sexid WHERE VisitorID = @visitorid";
                var count = await connection.ExecuteAsync(sQuery, new { sexid = visitor.SexID, visitorid = visitor.VisitorID });
                return count > 0;
            };
        }
    }
}
