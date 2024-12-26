using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Member:BaseEntity
    {
        //public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [MaxLength(50)]
        public string? Name { get; set; }
        [Required]
        
        public string? Email { get; set; }
        public List<Project>? projects { get; set; }
        public List<TaskMod>? tasks { get; set; }

        public List<Leader>? leaders { get; set; }

    }
}
