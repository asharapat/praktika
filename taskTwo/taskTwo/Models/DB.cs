using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace taskTwo.Models
{
    public class DB
    {
        public int supplyID { get; set; }
        public int supplyAmount { get; set; }
        public string supplyMonth { get; set; }
        public string supplyRegion { get; set; }
        //public int supplyRegionId { get; set; }
        //public int supplyProductId { get; set; }
        //public int supplyCategoriesId { get; set; }
        public string supplyPName { get; set; }
        public string supplyCNumber { get; set; }
        public int costAmount1 { get; set; }
        public string costMonth1 { get; set; }
        public string costRegion { get; set; }
        public string costProduct { get; set; }
    }
}