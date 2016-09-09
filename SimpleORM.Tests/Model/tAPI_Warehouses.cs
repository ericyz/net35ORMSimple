using System;
namespace SimpleORM.Model {
	[SimpleORM.Annotation.Table("tAPI_Warehouses")]
	public class tAPI_Warehouses{
		[SimpleORM.Annotation.Column("LocationID")]
		public Int32 LocationID {get;set;}
		[SimpleORM.Annotation.Column("WHCode")]
		public String WHCode {get;set;}
		[SimpleORM.Annotation.Column("WHName")]
		public String WHName {get;set;}
		[SimpleORM.Annotation.Column("Street")]
		public String Street {get;set;}
		[SimpleORM.Annotation.Column("Block")]
		public String Block {get;set;}
		[SimpleORM.Annotation.Column("City")]
		public String City {get;set;}
		[SimpleORM.Annotation.Column("State")]
		public String State {get;set;}
		[SimpleORM.Annotation.Column("PostCode")]
		public String PostCode {get;set;}
		[SimpleORM.Annotation.Column("Country")]
		public String Country {get;set;}
		[SimpleORM.Annotation.Column("IsActive")]
		public String IsActive {get;set;}
	}
}
