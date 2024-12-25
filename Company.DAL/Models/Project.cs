using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Project:BaseEntity
    {
        //public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public String? status { get; set; }
        public List<Member>? members { get; set; }
        public List<TaskMod>? tasks { get; set; }
        public Leader Leader { get; set; }
        //public int LeaderId { get; set; }
    }
}
