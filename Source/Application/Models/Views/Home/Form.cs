using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Application.Models.Views.Home
{
	public class Form
	{
		#region Properties

		[Required]
		public virtual LogLevel Level { get; set; } = LogLevel.Information;

		public virtual IList<SelectListItem> Levels { get; } = new List<SelectListItem>();

		[Required]
		public virtual string Message { get; set; } = "fe217acf-389f-4cfd-ae94-e62a580f1ebd";

		#endregion
	}
}