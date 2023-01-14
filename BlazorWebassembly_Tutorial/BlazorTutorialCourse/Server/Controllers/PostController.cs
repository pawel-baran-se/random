using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
using System.Runtime.CompilerServices;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly AppDBContext _appDBContext;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IMapper _mapper;

		public PostController(AppDBContext appDBContext, IWebHostEnvironment webHostEnvironment, IMapper mapper)
		{
			_appDBContext = appDBContext;
			_webHostEnvironment = webHostEnvironment;
			_mapper = mapper;
		}


		[HttpGet]
		public async Task<IActionResult> Get()
		{
			List<Post> Posts = await _appDBContext.Posts
				.Include(p => p.Category)
				.ToListAsync();

			return Ok(Posts);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<Post>> GetById(int id)
		{

			Post Post = await GetPostByPostId(id);

			return Ok(Post);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] PostDto postToCreateDto)
		{
			try
			{
				if (postToCreateDto == null)
				{
					return BadRequest(ModelState);
				}

				if (ModelState.IsValid == false)
				{
					return BadRequest(ModelState);
				}

				Post postToCreate = _mapper.Map<Post>(postToCreateDto);

				if (postToCreate.Published == true)
				{
					postToCreate.PublishDate = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm");
				}

				await _appDBContext.Posts.AddAsync(postToCreate);

				bool changesPersistedToDatabase = await PersistChangesToDatabase();

				if (changesPersistedToDatabase == false)
				{
					return StatusCode(500, $"Something went wrong on our side. Please contact the administrator.");
				}
				else
				{
					return Created("Create", postToCreate);
				}
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Something went wrong on our side. Please contact the administrator. Error message: {e.Message}.");
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] PostDto updatedPostDTO)
		{
			try
			{
				if (id < 1 || updatedPostDTO == null || id != updatedPostDTO.PostId)
				{
					return BadRequest(ModelState);
				}

				Post oldPost = await _appDBContext.Posts.FindAsync(id);

				if (oldPost == null)
				{
					return NotFound();
				}

				if (ModelState.IsValid == false)
				{
					return BadRequest(ModelState);
				}

				Post updatedPost = _mapper.Map<Post>(updatedPostDTO);

				if (updatedPost.Published == true)
				{
					if (oldPost.Published == false)
					{
						updatedPost.PublishDate = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm");
					}
					else
					{
						updatedPost.PublishDate = oldPost.PublishDate;
					}
				}
				else
				{
					updatedPost.PublishDate = string.Empty;
				}

				// Detach oldPost from EF, else it can't be updated.
				_appDBContext.Entry(oldPost).State = EntityState.Detached;

				_appDBContext.Posts.Update(updatedPost);

				bool changesPersistedToDatabase = await PersistChangesToDatabase();

				if (changesPersistedToDatabase == false)
				{
					return StatusCode(500, "Something went wrong on our side. Please contact the administrator.");
				}
				else
				{
					return Created("Create", updatedPost);
				}
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Something went wrong on our side. Please contact the administrator. Error message: {e.Message}.");
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				if (id < 1)
				{
					return BadRequest(ModelState);
				}

				bool exists = await _appDBContext.Posts.AnyAsync(Post => Post.PostId == id);

				if (exists == false)
				{
					return NotFound();
				}

				if (ModelState.IsValid == false)
				{
					return BadRequest(ModelState);
				}

				Post PostToDelete = await GetPostByPostId(id);

				if (PostToDelete.ThumbnailImagePath != "uploads/placeholder.jpg")
				{
					string fileName = PostToDelete.ThumbnailImagePath.Split('/').Last();

					System.IO.File.Delete($"{_webHostEnvironment.ContentRootPath}\\wwwroot\\uploads\\{fileName}");
				}

				_appDBContext.Posts.Remove(PostToDelete);

				bool changesPersistedToDatabase = await PersistChangesToDatabase();

				if (changesPersistedToDatabase == false)
				{
					return StatusCode(500, "Something went wrong on our side. Please contact the administrator.");
				}
				else
				{
					return NoContent();
				}
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Something went wrong on our side. Please contact the administrator. Error message: {e.Message}.");
			}
		}

		#region Utility methods

		[NonAction]
		[ApiExplorerSettings(IgnoreApi = true)]
		private async Task<bool> PersistChangesToDatabase()
		{
			int amountOfChanges = await _appDBContext.SaveChangesAsync();

			return amountOfChanges > 0;
		}

		[NonAction]
		[ApiExplorerSettings(IgnoreApi = true)]
		private async Task<Post> GetPostByPostId(int PostId)
		{
			Post PostToGet = await _appDBContext.Posts
					.Include(p => p.Category)
					.FirstAsync(p => p.PostId == PostId);

			return PostToGet;
		}

		#endregion


	}
}
