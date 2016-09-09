using System;
namespace SimpleORM.Model {
	[SimpleORM.Annotation.Table("tAPI_Sales_HDR")]
	public class tAPI_Sales_HDR{
		[SimpleORM.Annotation.Column("DocEntry")]
		public Int32 DocEntry {get;set;}
		[SimpleORM.Annotation.Column("DocNum")]
		public String DocNum {get;set;}
		[SimpleORM.Annotation.Column("DocDate")]
		public DateTime DocDate {get;set;}
		[SimpleORM.Annotation.Column("DueDate")]
		public DateTime DueDate {get;set;}
		[SimpleORM.Annotation.Column("CardCode")]
		public String CardCode {get;set;}
		[SimpleORM.Annotation.Column("TotalPrice")]
		public Double TotalPrice {get;set;}
		[SimpleORM.Annotation.Column("Discount")]
		public Double Discount {get;set;}
		[SimpleORM.Annotation.Column("DocTotal")]
		public Double DocTotal {get;set;}
		[SimpleORM.Annotation.Column("CustReference")]
		public String CustReference {get;set;}
		[SimpleORM.Annotation.Column("Address")]
		public String Address {get;set;}
		[SimpleORM.Annotation.Column("Status")]
		public String Status {get;set;}
		[SimpleORM.Annotation.Column("Comments")]
		public String Comments {get;set;}
		[SimpleORM.Annotation.Column("Location")]
		public String Location {get;set;}
		[SimpleORM.Annotation.Column("RunNo")]
		public String RunNo {get;set;}
		[SimpleORM.Annotation.Column("ShipVia")]
		public String ShipVia {get;set;}
		[SimpleORM.Annotation.Column("FreightAmount")]
		public Decimal FreightAmount {get;set;}
		[SimpleORM.Annotation.Column("ShippingAddr1")]
		public String ShippingAddr1 {get;set;}
		[SimpleORM.Annotation.Column("ShippingAddr2")]
		public String ShippingAddr2 {get;set;}
		[SimpleORM.Annotation.Column("ShippingCity")]
		public String ShippingCity {get;set;}
		[SimpleORM.Annotation.Column("ShippingState")]
		public String ShippingState {get;set;}
		[SimpleORM.Annotation.Column("ShippingPostCode")]
		public String ShippingPostCode {get;set;}
		[SimpleORM.Annotation.Column("CardName")]
		public String CardName {get;set;}
		[SimpleORM.Annotation.Column("Weight")]
		public Double Weight {get;set;}
		[SimpleORM.Annotation.Column("Volumn")]
		public Double Volumn {get;set;}
	}
}
