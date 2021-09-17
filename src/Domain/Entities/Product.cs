using System;
using System.Collections.Generic;
using CleanArchitectureBase.Domain.Common;

namespace CleanArchitectureBase.Domain.Entities
{
    public class Product : AuditableEntity, IHasDomainEvent
    {
        public int IDParent { get; set; }
        public string SKU { get; set; }
        public string? EAN { get; set; }
        public string? UPC { get; set; }
        public string? Manufacturernumber { get; set; }
        public string? Taricnumber { get; set; }
        public string? ISBN { get; set; }
        public string? ASIN { get; set; }
        public string? FNSKU { get; set; }
        public string? UNNumber { get; set; }
        public string? Hazardnumber { get; set; }
        public string? Comment { get; set; }
        public string? Searchingterms { get; set; }
        public int Type { get; set; }
        public int Active { get; set; }
        public int Deleted { get; set; }
        public int StockActive { get; set; }
        public int StockSplitable { get; set; }
        public int IsParent { get; set; }
        public int IsPartlist { get; set; }
        public int IsSerialnumber { get; set; }
        public int IsExpirationDate { get; set; }
        public int IsBatches { get; set; }
        public double Netprice { get; set; }
        public double NetPurchasingPrice { get; set; }
        public double StockReserved { get; set; }
        public double StockIntake { get; set; }
        public double StockBlocked { get; set; }
        public double StockAvaiable { get; set; }
        public double StockTotal { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}
