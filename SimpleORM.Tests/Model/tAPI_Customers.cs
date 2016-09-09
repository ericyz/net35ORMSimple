using System;
namespace SimpleORM.Model {
	[SimpleORM.Annotation.Table("tAPI_Customers")]
	public class tAPI_Customers{
		[SimpleORM.Annotation.Column("AccNo")]
		public Int32 AccNo {get;set;}
		[SimpleORM.Annotation.Column("CardCode")]
		public String CardCode {get;set;}
		[SimpleORM.Annotation.Column("Name")]
		public String Name {get;set;}
		[SimpleORM.Annotation.Column("Address1")]
		public String Address1 {get;set;}
		[SimpleORM.Annotation.Column("Address2")]
		public String Address2 {get;set;}
		[SimpleORM.Annotation.Column("Address3")]
		public String Address3 {get;set;}
		[SimpleORM.Annotation.Column("Address4")]
		public String Address4 {get;set;}
		[SimpleORM.Annotation.Column("Address5")]
		public String Address5 {get;set;}
		[SimpleORM.Annotation.Column("DelAddr1")]
		public String DelAddr1 {get;set;}
		[SimpleORM.Annotation.Column("DelAddr2")]
		public String DelAddr2 {get;set;}
		[SimpleORM.Annotation.Column("DelAddr3")]
		public String DelAddr3 {get;set;}
		[SimpleORM.Annotation.Column("DelAddr4")]
		public String DelAddr4 {get;set;}
		[SimpleORM.Annotation.Column("DelAddr5")]
		public String DelAddr5 {get;set;}
		[SimpleORM.Annotation.Column("Country")]
		public String Country {get;set;}
		[SimpleORM.Annotation.Column("Phone1")]
		public String Phone1 {get;set;}
		[SimpleORM.Annotation.Column("Phone2")]
		public String Phone2 {get;set;}
		[SimpleORM.Annotation.Column("EMail")]
		public String EMail {get;set;}
		[SimpleORM.Annotation.Column("Contact")]
		public String Contact {get;set;}
		[SimpleORM.Annotation.Column("Notes")]
		public String Notes {get;set;}
		[SimpleORM.Annotation.Column("Type")]
		public String Type {get;set;}
		[SimpleORM.Annotation.Column("IsActive")]
		public String IsActive {get;set;}
		[SimpleORM.Annotation.Column("PriceLevel")]
		public String PriceLevel {get;set;}
		[SimpleORM.Annotation.Column("TaxZone")]
		public String TaxZone {get;set;}
	}
}
