namespace alibaba.Sql.Data
{
    using Microsoft.Extensions.Options;
    using MySql.Data.MySqlClient;
    using System.Data;
    using System.Data.SqlClient;
    using alibaba.Common;

    public class SqlDbSettings : IDbSettings
    {
        private readonly DbSettings _dbSettings;

        public SqlDbSettings(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        public IDbConnection Connection()
        {
            return new MySqlConnection(_dbSettings.DbConnection);
        }
    }
}
