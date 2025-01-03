﻿using System.Security.Claims;
using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.Helper;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper autoMapper;
        public static string CurrentMemberEmail;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork, IMapper mapper, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.unitOfWork = unitOfWork;
			autoMapper = mapper;
			this.roleManager = roleManager;
			_httpContextAccessor = httpContextAccessor;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
            if (ModelState.IsValid)
            {
                var check = await userManager.FindByEmailAsync(registerViewModel.Email);
				if (check == null)
				{
					var newUser = autoMapper.Map<RegisterViewModel, User>(registerViewModel);
					newUser.UserName = registerViewModel.FirstName + "_" + registerViewModel.LastName;
					var result = await userManager.CreateAsync(newUser, registerViewModel.Password);
                    
                    if (result.Succeeded)
					{
                        await userManager.AddToRoleAsync(newUser, registerViewModel.Role);


                        if (registerViewModel.Role == "Leader")
						{
                            var project = unitOfWork.ProjectRepository.Get(registerViewModel.ProjectId.Value);
							if (project.status != "Active")
							{
								Leader leader = new Leader()
								{
									Name = registerViewModel.FirstName + " " + registerViewModel.LastName,
									Email = registerViewModel.Email,
									ProjectId = registerViewModel.ProjectId.Value,
								};
								unitOfWork.LeaderReposatory.Insert(leader);


								project.status = "Active";
								unitOfWork.ProjectRepository.Update(project);
							}
							else return RedirectToAction("Error","Home");

                        }
                        else
                        {
							Member member = new Member()
							{
								Name = registerViewModel.FirstName + " " + registerViewModel.LastName,
								Email = registerViewModel.Email,
							};
							unitOfWork.MemberReposatory.Insert(member);
                        }
						unitOfWork.Complete();

                        return RedirectToAction("LogIn");
					}
					return RedirectToAction("Error", "Home");
                }
				return RedirectToAction("LogIn");
            }
			return RedirectToAction("Error", "Home");
        }
		[HttpGet]
		public IActionResult LogIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
		{
			if (ModelState.IsValid)
			{
				var check = await userManager.FindByEmailAsync(logInViewModel.Email);
				if(check != null)
				{
					var checkPassword = await userManager.CheckPasswordAsync(check, logInViewModel.Password);
					if (checkPassword)
					{
						var result = await signInManager.PasswordSignInAsync(check, logInViewModel.Password, logInViewModel.RememberMe,false);
                        if (result.Succeeded)
                        {
                            if (check.Role == "Member")
                            {
								return RedirectToAction("ShowTasks", "Member");
                            }
							return RedirectToAction("ShowTasks", "Leader");
						}
						return RedirectToAction("Error", "Home");
                    }
					return View(NotFound("Wrong Password"));
				}
				return RedirectToAction("Register");
			}
			return RedirectToAction("Error", "Home");
        }
		[HttpGet]
		public IActionResult SignOut()
		{
			signInManager.SignOutAsync();
			return View("LogIn");
		}
		public IActionResult Home()
		{
            var currentUser = _httpContextAccessor?.HttpContext?.User;
            if (currentUser != null)
			{
                var CurrentRole = currentUser?.FindFirst(ClaimTypes.Role)?.Value;
                if (CurrentRole == "Leader")
				{
					return RedirectToAction("ShowTasks", "Leader");
				}
				return RedirectToAction("ShowTasks", "Member");

            }

            return RedirectToAction("LogIn");
		}
	}
}
