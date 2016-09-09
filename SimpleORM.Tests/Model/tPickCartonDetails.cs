using System;
namespace SimpleORM.Model {
	[SimpleORM.Annotation.Table("tPickCartonDetails")]
	public class tPickCartonDetails{
		[SimpleORM.Annotation.Column("PickHeaderGUID")]
		public Guid PickHeaderGUID {get;set;}
		[SimpleORM.Annotation.Column("PickID")]
		public Int32 PickID {get;set;}
		[SimpleORM.Annotation.Column("PickEntry")]
		public Int32 PickEntry {get;set;}
		[SimpleORM.Annotation.Column("CartonNumber")]
		public Int32 CartonNumber {get;set;}
		[SimpleORM.Annotation.Column("CartonQty")]
		public Decimal CartonQty {get;set;}
		[SimpleORM.Annotation.Column("SSCCFullBarCode")]
		public String SSCCFullBarCode {get;set;}
	}
}
