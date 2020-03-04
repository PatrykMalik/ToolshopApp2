using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolshopApp2.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Emial { get; set; }
        public int KindOfUserId { get; set; }
        public KindOfUser KindOfUser { get; set; }
    }
}
