using System.Configuration;
using SimpleORM.Util;

namespace SampleApplication.Util {
    public class DBInitializer {
        public static void IntialDB()
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            string sql = @"if not exists (select * from dbo.sysobjects where id = object_id(N'[Employee]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE Employee(
	Id INT IDENTITY NOT NULL PRIMARY KEY ,
	FirstName VARCHAR(100) NOT NULL,
	MiddleName VARCHAR(100)  NULL,
	LastName VARCHAR(100)  NULL,
	DateOfBirth DATETIME NOT NULL
);";
            SQLExecutor.ExecuteNonQuery(sql, connectionString);
        }
    }
}
