using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolshopApp2.Model
{
    public class DishwashersOnStock
    {        
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Pnc { get; set; }
        public string Elc { get; set; }
        public string SerialNumber { get; set; }
        public string Assigment { get; set; }
        public DateTime ScanDate { get; set; }
        public string BegingSrz { get; set; }
        public string EndingSrz { get; set; }
        public string Comments { get; set; }
    }
}
