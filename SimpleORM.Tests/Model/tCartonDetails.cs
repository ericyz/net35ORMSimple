using System;
namespace SimpleORM.Model {
	[SimpleORM.Annotation.Table("tCartonDetails")]
	public class tCartonDetails{
		[SimpleORM.Annotation.Column("Name")]
		public String Name {get;set;}
		[SimpleORM.Annotation.Column("Length")]
		public Decimal Length {get;set;}
		[SimpleORM.Annotation.Column("Width")]
		public Decimal Width {get;set;}
		[SimpleORM.Annotation.Column("Height")]
		public Decimal Height {get;set;}
		[SimpleORM.Annotation.Column("Priority")]
		public Int32 Priority {get;set;}
		[SimpleORM.Annotation.Column("Weight")]
		public Decimal Weight {get;set;}
	}
}
