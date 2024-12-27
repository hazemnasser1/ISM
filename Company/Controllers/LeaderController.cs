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
    [Authorize(Roles ="Leader")]
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
            if (task == null)
                return BadRequest("Task cannot be null.");

            var currentUser = _httpContextAccessor.HttpContext?.User;
            var userEmail = currentUser?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized("User email not found.");

            var user = unitOfWork.LeaderReposatory.GetByEmail(userEmail);
            if (user == null)
                return NotFound("Leader not found.");

            var project = unitOfWork.ProjectRepository.GetByIDWithInclude(user.ProjectId);
            if (project == null)
                return NotFound("Project not found.");

            var member = unitOfWork.MemberReposatory.GetMemberWithInclude(task.MemberID); ;
            if (member == null)
                return NotFound("Member not found.");
            

            // Check if the member is already part of the project
            if (!project.members.Any(m => m.Id == member.Id))
            {
                // Add member to the project only if not already added
                project.members.Add(member);
            }
           
            // Check if the leader is already associated with the member
            if (!member.leaders.Any(l => l.Id == user.Id))
            {
                // Associate the leader with the member
                member.leaders.Add(user);
                user.members.Add(member);
            }

            // Map and add the task
            var ourTask = autoMapper.Map<TaskViewModel, TaskMod>(task);
            if (ourTask == null)
                return BadRequest("Task mapping failed.");

            project.tasks.Add(ourTask);

            // Persist changes
            try
            {
                unitOfWork.ProjectRepository.Update(project);
                unitOfWork.TaskRepository.Insert(ourTask);
                unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // Handle exception and rollback if necessary
                // Log exception (not shown here)
                return StatusCode(500, "An error occurred while saving the task.");
            }

            return RedirectToAction("ShowTasks");
        }

        [HttpPost]
        public IActionResult DeleteTask(string TaskName)
        {
            var task=unitOfWork.TaskRepository.GetTaskByName(TaskName);
            unitOfWork.TaskRepository.Delete(task);
            unitOfWork.Complete();
            return RedirectToAction("ShowTasks");
            
        }
        [HttpGet]
        public IActionResult AddTeamMember()
        {
            var CurrentUser = _httpContextAccessor.HttpContext?.User;
            var userEmail = CurrentUser?.FindFirst(ClaimTypes.Email)?.Value;
            var user = unitOfWork.LeaderReposatory.GetByEmail(userEmail);

            string projectName = user.Project.Name;
            return View((object) projectName);
        }
        [HttpPost]
        public IActionResult AddTeamMember(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email cannot be null or empty.");

            var currentUser = _httpContextAccessor.HttpContext?.User;
            var userEmail = currentUser?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized("User email not found.");

            var user = unitOfWork.LeaderReposatory.GetByEmail(userEmail);
            if (user == null)
                return NotFound("Leader not found.");

            var member = unitOfWork.MemberReposatory.GetMemberWithEmail(email);
            if (member == null)
                return NotFound("Member not found with the provided email.");

            var project = unitOfWork.ProjectRepository.GetByIDWithInclude(user.ProjectId);
            if (project == null)
                return NotFound("Project not found for the current leader.");

  
            if (!project.members.Any(m => m.Id == member.Id))
            {

                project.members.Add(member);
            }

            if (!member.leaders.Any(l => l.Id == user.Id))
            {
                member.leaders.Add(user);
                user.members.Add(member);
            }

            try
            {
                unitOfWork.ProjectRepository.Update(project);
                unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the team member.");
            }

            return RedirectToAction("ShowTasks");
        }
        [HttpGet]
        public IActionResult ShowTeamMember()
        {
            var CurrentUser = _httpContextAccessor.HttpContext?.User;
            var userEmail = CurrentUser?.FindFirst(ClaimTypes.Email)?.Value;
            var user = unitOfWork.LeaderReposatory.GetByEmail(userEmail);
            var members=user.members.ToList();
            return View(members);

        }

        [HttpGet]
        public IActionResult UpdateTask(string TaskName)
        {
            var task = unitOfWork.TaskRepository.GetTaskByName(TaskName);
            var ourTask = autoMapper.Map<TaskMod, TaskViewModel>(task);
            return View(ourTask);
            

        }

        [HttpPost]
        public IActionResult UpdateTask(TaskViewModel task)
        {
            var ourTask = unitOfWork.TaskRepository.GetTaskByName(task.Name);

            ourTask.isDone = task.isDone;
            ourTask.Description = task.Description;
            ourTask.MemberID = task.MemberID;
            unitOfWork.TaskRepository.Update(ourTask);
            unitOfWork.Complete();
            return RedirectToAction("ShowTasks");

        }
    }
}
