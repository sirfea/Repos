//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MoviesWebSite.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ViewOrderDetail
    {
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public decimal MovieCost { get; set; }
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public System.DateTime DateTime { get; set; }
    }
}