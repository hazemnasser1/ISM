using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Leader:BaseEntity
    {
        //public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50)]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        
        public Project? Project { get; set;}
        public int ProjectId { get; set; }
    }
}
