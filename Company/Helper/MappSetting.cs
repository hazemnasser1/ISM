using AutoMapper;
using Company.DAL.Models;
using Company.PL.ViewModels;

namespace Company.PL.Helper
{
	public class MappSetting: Profile
	{
		public MappSetting() 
		{
			CreateMap<RegisterViewModel,User>().ReverseMap();
			CreateMap<TaskViewModel,TaskMod>().ReverseMap();
		}
	}
}
