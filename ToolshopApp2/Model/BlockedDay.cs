using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolshopApp2.Model
{
    class BlockedDay
    {
        [Key]
        public int Id { get; set; }
        public DateTime blockedDate { get; set; }
    }
}
