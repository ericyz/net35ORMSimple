using SimpleORM.Model;
namespace SimpleORM.DataAccess {
	public class EmployeeService : DataAccess<Employee>{
		public EmployeeService(string connStr): base(connStr){}
	}
}
