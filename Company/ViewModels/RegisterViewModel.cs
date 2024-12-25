using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Role { get; set; }
		public int Age { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
		public string ConfirmedPassword { get; set; }
		public int? ProjectId { get; set; }
	}
}
