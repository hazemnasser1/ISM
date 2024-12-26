using Company.DAL.Models;

namespace Company.PL.ViewModels
{
    public class ProjectMembers
    {
        public string Name { get; set; }
        public List<Member> members { get; set; } = new List<Member>();
    }
}
