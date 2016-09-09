using SimpleORM.Model;
namespace SimpleORM.DataAccess {
	public class tCartonDetailsService : DataAccess<tCartonDetails>{
		public tCartonDetailsService(string connStr): base(connStr){}
	}
}
