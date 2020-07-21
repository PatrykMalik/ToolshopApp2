using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolshopApp2.Model
{
    public class RequestStatus
    {

        
        public int Id { get; set; }       
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}
