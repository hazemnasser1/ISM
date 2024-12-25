using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Company.DAL.Models
{
    public class TaskMod:BaseEntity
    {
        //public int Id { get; set; }


        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool isDone { get; set; }
        public Member? member { get; set; }

        public int MemberID { get; set; }
        public Project? project { get; set; }

    }
}
