//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace taskTwo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cost
    {
        public int costId { get; set; }
        public int costAmount { get; set; }
        public string Month { get; set; }
        public int productId { get; set; }
        public int regionId { get; set; }
    
        public virtual product product { get; set; }
        public virtual region region { get; set; }
    }
}
