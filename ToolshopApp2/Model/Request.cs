using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToolshopApp2.Model
{
    public class Request //: INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public string Classyfy{ get; set;}
        public string Order{ get; set;}
        public DateTime Date{ get; set;}
        public string Project{ get; set;}
        public string Description{ get; set;}
        public string CostCenter{ get; set;}
        public bool Attachment{ get; set;}
        public string ContactPerson{ get; set;}
        public string Email{ get; set;}
        public string Address{ get; set;}
        public string BeginigSrz{ get; set;}
        public string EndingSrz{ get; set;}
        public string Transpot{ get; set;}
        public bool Insurance{ get; set;}
        public string InsuranceCost{ get; set;}
        public string DescpriptionToolshop { get; set; }
        public DateTime CreationTime { get; set; }
        public string Status { get; set; }

        

    }
}
//public event PropertyChangedEventHandler PropertyChanged;
//private int id;
//private string user;
//private string classyfy;
//private string order;
//private DateTime date;
//private string project;
//private string description;
//private string costCenter;
//private bool attachment;
//private string contactPerson;
//private string email;
//private string address;
//private string beginigSrz;
//private string endingSrz;
//private string transpot;
//private bool insurance;
//private string insuranceCost;

//public int Id { get => id; set { id = value; OnPropertyChanged(); } }
//public string User { get => user; set { user = value; OnPropertyChanged(); } }
//public string Classyfy { get => classyfy; set { classyfy = value; OnPropertyChanged(); } }
//public string Order { get => order; set { order = value; OnPropertyChanged(); } }
//public DateTime Date { get => date; set { date = value; OnPropertyChanged(); } }
//public string Project { get => project; set { project = value; OnPropertyChanged(); } }
//public string Description { get => description; set { description = value; OnPropertyChanged(); } }
//public string CostCenter { get => costCenter; set { costCenter = value; OnPropertyChanged(); } }
//public bool Attachment { get => attachment; set { attachment = value; OnPropertyChanged(); } }
//public string ContactPerson { get => contactPerson; set { contactPerson = value; OnPropertyChanged(); } }
//public string Email { get => email; set { email = value; OnPropertyChanged(); } }
//public string Address { get => address; set { address = value; OnPropertyChanged(); } }
//public string BeginigSrz { get => beginigSrz; set { beginigSrz = value; OnPropertyChanged(); } }
//public string EndingSrz { get => endingSrz; set { endingSrz = value; OnPropertyChanged(); } }
//public string Transpot { get => transpot; set { transpot = value; OnPropertyChanged(); } }
//public bool Insurance { get => insurance; set { insurance = value; OnPropertyChanged(); } }
//public string InsuranceCost { get => insuranceCost; set { insuranceCost = value; OnPropertyChanged(); } }
//public string Srz1 { get; set; }
//public string Srz2 { get; set; }
//public string Srz3 { get; set; }
//public string Srz4 { get; set; }
//public string Srz5 { get; set; }
//public string Swz1 { get; set; }
//public string Swz2 { get; set; }
//public string Swz3 { get; set; }
//public string Swz4 { get; set; }
//public string Swz5 { get; set; }
//public string Time { get; set; }
//private void OnPropertyChanged([CallerMemberName] string name = null)
//{
//    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
//}