using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolshopApp2.Model
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public string Classyfy { get; set; }
        public string Order { get; set; }
        public DateTime Date { get; set; }
        public string Project { get; set; }
        public string Description { get; set; }
        public string CostCenter { get; set; }
        public int RequestStatusesId { get; set; }
        public bool Attachment { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string BeginigSrz { get; set; }
        public string EndingSrz { get; set; }
        public string Transpot { get; set; }
        public bool Insurance { get; set; }
        public string InsuranceCost { get; set; }
        public string Srz1 { get; set; }
        public string Srz2 { get; set; }
        public string Srz3 { get; set; }
        public string Srz4 { get; set; }
        public string Srz5 { get; set; }
        public string Swz1 { get; set; }
        public string Swz2 { get; set; }
        public string Swz3 { get; set; }
        public string Swz4 { get; set; }
        public string Swz5 { get; set; }
        public string Time { get; set; }
        public string DescpriptionToolshop { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
