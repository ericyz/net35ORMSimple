using System;
namespace SimpleORM.Model {
	[SimpleORM.Annotation.Table("tAPI_ShippingTypes")]
	public class tAPI_ShippingTypes{
		[SimpleORM.Annotation.Column("AbsEntry")]
		public Int32 AbsEntry {get;set;}
		[SimpleORM.Annotation.Column("ShipVia")]
		public String ShipVia {get;set;}
		[SimpleORM.Annotation.Column("Description")]
		public String Description {get;set;}
		[SimpleORM.Annotation.Column("Calendar")]
		public String Calendar {get;set;}
		[SimpleORM.Annotation.Column("CalculationMethod")]
		public String CalculationMethod {get;set;}
		[SimpleORM.Annotation.Column("BaseRate")]
		public Decimal BaseRate {get;set;}
		[SimpleORM.Annotation.Column("TaxCategory")]
		public String TaxCategory {get;set;}
		[SimpleORM.Annotation.Column("FreightSalesAccount")]
		public String FreightSalesAccount {get;set;}
		[SimpleORM.Annotation.Column("FreightExpenseAccount")]
		public String FreightExpenseAccount {get;set;}
		[SimpleORM.Annotation.Column("Extra1")]
		public String Extra1 {get;set;}
		[SimpleORM.Annotation.Column("Extra2")]
		public String Extra2 {get;set;}
		[SimpleORM.Annotation.Column("Extra3")]
		public String Extra3 {get;set;}
	}
}
