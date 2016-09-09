using SimpleORM.Model;
namespace SimpleORM.DataAccess {
	public class tAPI_CustomersService : DataAccess<tAPI_Customers>{
		public tAPI_CustomersService(string connStr): base(connStr){}
	}
}
