using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly AppDBContext _appDBContext;

		public CategoriesController(AppDBContext appDBContext)
		{
			_appDBContext = appDBContext;
		}


		[HttpGet]
		public async Task<IActionResult> Get()
		{

			List<Category> categories = await _appDBContext.Categories.ToListAsync();
			return Ok(categories);
		}

		[HttpGet("withposts")]
		public async Task<IActionResult> GetWithPosts(bool withPosts)
		{

			List<Category> categories = await _appDBContext.Categories
				.Include(c => c.Posts)
				.ToListAsync();

			return Ok(categories);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Category>> GetById(int id)
		{

			Category category = await GetCategoryByCategoryId(id, false);

			return Ok(category);
		}

		[HttpGet("withposts/{id}")]
		public async Task<ActionResult<Category>> GetByIdWithPost(int id)
		{

			Category category = await GetCategoryByCategoryId(id, true);

			return Ok(category);
		}



		[NonAction]
		[ApiExplorerSettings(IgnoreApi = true)]
		private async Task<bool> PersistChangesToDatabase()
		{
			int amountOfChanges = await _appDBContext.SaveChangesAsync();

			return amountOfChanges > 0;
		}

		[NonAction]
		[ApiExplorerSettings(IgnoreApi = true)]
		private async Task<Category> GetCategoryByCategoryId(int categoryId, bool withPosts)
		{
			Category categoryToGet = null;

			if (withPosts == true)
			{
				categoryToGet = await _appDBContext.Categories
					.Include(c => c.Posts)
					.FirstAsync(c => c.CategoryId == categoryId);
			}
			else
			{
				categoryToGet = await _appDBContext.Categories
					.FirstAsync(c => c.CategoryId == categoryId);
			}

			return categoryToGet;
		}
	}
}
