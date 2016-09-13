using System.Collections.Generic;
using System.Linq;
using SimpleORM.Model;
namespace SimpleORM.DataAccess {
	public class EmployeeService : DataAccess<Employee>{
		public EmployeeService(string connStr): base(connStr){}

	    public void DeleteById(int id)
	    {
            DeleteById(id.ToString());
//	        DeleteByKey(new Dictionary<string, object>(){ { "Id", id } });
	    }
        public void DeleteById(string id)
	    {
	        DeleteByKey(new Dictionary<string, object>(){ { "Id", id } });
	    }

	    public Employee ReadById(string id)
	    {
	        return ReadByKey(new Dictionary<string, object>() { { "Id", id } }).FirstOrDefault();
	    }
	}
}
