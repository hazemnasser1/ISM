using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Company.PL.Controllers
{
    [Authorize(Roles ="Member")]
    public class MemberController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper autoMapper;
        public static string CurrentMemberEmail;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public MemberController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this._httpContextAccessor = httpContextAccessor;
            this.autoMapper = autoMapper;
        }
        public IActionResult ShowTasks()
        {
            var currentUser = _httpContextAccessor?.HttpContext?.User;
            CurrentMemberEmail = currentUser?.FindFirst(ClaimTypes.Email)?.Value;
            var member = unitOfWork.MemberReposatory.GetMemberWithEmail(CurrentMemberEmail);
            var tasks = unitOfWork.MemberReposatory.Showtasks(member);
            return View(tasks);
        }
        [HttpPost]
        public IActionResult MarkAsDone(int id)
        {
            var task = unitOfWork.TaskRepository.Get(id);
            if (task != null)
            {
                task.isDone = true;
                unitOfWork.TaskRepository.Update(task);
                unitOfWork.Complete();
                return RedirectToAction("ShowTasks");
            }
            return RedirectToAction("Error", "Home");
        }
        public IActionResult showTeamMembers()
        {
            var member = unitOfWork.MemberReposatory.GetMemberWithEmail(CurrentMemberEmail);
            var teamMembers = new List<ProjectMembers>();
            foreach(var project in member.projects)
            {
                var projectMembers = new ProjectMembers();
                var result = unitOfWork.MemberReposatory.showteam(project.Id);
                projectMembers.Name = project.Name;
                projectMembers.members.AddRange(result);
                projectMembers.members.Remove(member);
                teamMembers.Add(projectMembers);
            }
            return View(teamMembers);
        }
    }
}
