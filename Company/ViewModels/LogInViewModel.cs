using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
	public class LogInViewModel
	{
		public string Email { get; set; }
        [DataType(DataType.Password)]

        public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
