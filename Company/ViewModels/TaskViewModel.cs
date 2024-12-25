using System.Security.Cryptography.X509Certificates;

namespace Company.PL.ViewModels
{
    public class TaskViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MemberID {  get; set; }
        public bool isDone { get; set; }
    }
}
