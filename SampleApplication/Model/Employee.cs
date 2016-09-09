using System;
using SimpleORM.Annotation;

namespace SimpleORM.Model {
	[SimpleORM.Annotation.Table("Employee")]
	public class Employee{
		[SimpleORM.Annotation.Column("Id")]
        [DataBaseGenerated]
		public Int32 Id {get;set;}
		[SimpleORM.Annotation.Column("FirstName")]
		public String FirstName {get;set;}
		[SimpleORM.Annotation.Column("MiddleName")]
		public String MiddleName {get;set;}
		[SimpleORM.Annotation.Column("LastName")]
		public String LastName {get;set;}
		[SimpleORM.Annotation.Column("DateOfBirth")]
		public DateTime DateOfBirth {get;set;}
	}
}
