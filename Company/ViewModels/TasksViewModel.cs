using System.Security.Cryptography.X509Certificates;

namespace Company.PL.ViewModels
{
    public class TasksViewModel
    {
        public List<TaskViewModel> Tasks { get; set; }

        public TaskViewModel task { get; set; }
    }
}
