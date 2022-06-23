using Application.Models.Views.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Application.Controllers
{
	public class HomeController : Controller
	{
		#region Constructors

		public HomeController(ILoggerFactory loggerFactory)
		{
			this.Logger = (loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory))).CreateLogger(this.GetType());
		}

		#endregion

		#region Properties

		protected internal virtual ILogger Logger { get; }

		#endregion

		#region Methods

		protected internal virtual async Task<HomeViewModel> CreateHomeViewModelAsync(Form form = null)
		{
			var model = new HomeViewModel();

			if(form != null)
				model.Form = form;

			foreach(var level in Enum.GetValues<LogLevel>())
			{
				var text = level.ToString();
				var item = new SelectListItem(text, text, model.Form.Level == level);
				model.Form.Levels.Add(item);
			}

			return await Task.FromResult(model);
		}

		[HttpGet]
		public virtual async Task<IActionResult> Index()
		{
			return this.View(await this.CreateHomeViewModelAsync());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual async Task<IActionResult> Index(Form form)
		{
			if(form == null)
				throw new ArgumentNullException(nameof(form));

			var model = await this.CreateHomeViewModelAsync(form);

			// ReSharper disable InvertIf
			if(this.ModelState.IsValid)
			{
				this.Logger.Log(form.Level, form.Message);
				model.Confirmation = true;
			}
			// ReSharper restore InvertIf

			return await Task.FromResult(this.View(model));
		}

		#endregion
	}
}