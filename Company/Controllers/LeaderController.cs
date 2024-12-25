using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    [Authorize]
    public class LeaderController: Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper autoMapper;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public LeaderController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this._httpContextAccessor = httpContextAccessor;
            this.autoMapper = autoMapper;
        }

        [HttpGet]
        public IActionResult ShowTasks()
        {
            var CurrentUser = _httpContextAccessor.HttpContext?.User;
            var userEmail = CurrentUser?.FindFirst(ClaimTypes.Email)?.Value;
            var user = unitOfWork.LeaderReposatory.GetByEmail(userEmail);
            var task = unitOfWork.ProjectRepository.GetByIDWithInclude(user.ProjectId).tasks;
            var ourtask = autoMapper.Map<List<TaskMod>, List<TaskViewModel>>(task);
            
            
            TasksViewModel tasks = new TasksViewModel();

            tasks.Tasks = ourtask;
            
            

            return View(tasks);
        }
        [HttpPost]
        public IActionResult AddTask(TaskViewModel task)
        { 
            var CurrentUser = _httpContextAccessor.HttpContext?.User;
            var userEmail = CurrentUser?.FindFirst(ClaimTypes.Email)?.Value;
            var user=unitOfWork.LeaderReposatory.GetByEmail(userEmail);
            var project = unitOfWork.ProjectRepository.GetByIDWithInclude(user.ProjectId);
            var member = unitOfWork.MemberReposatory.Get(task.MemberID);
            project.members.Add(member);
            var ourtask=autoMapper.Map<TaskViewModel, TaskMod>(task);
            project.tasks.Add(ourtask);
            unitOfWork.ProjectRepository.Update(project);
            unitOfWork.TaskRepository.Insert(ourtask);
            unitOfWork.Complete();

            return View("ShowTasks");
        }
        [HttpPost]
        public IActionResult DeleteTask(string TaskName)
        {
            var task=unitOfWork.TaskRepository.GetTaskByName(TaskName);
            unitOfWork.TaskRepository.Delete(task);
            unitOfWork.Complete();
            return View("ShowTasks");
            
        }
        
    }
}
