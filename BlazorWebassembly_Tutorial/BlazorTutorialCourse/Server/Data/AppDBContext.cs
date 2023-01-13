using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System.Runtime.CompilerServices;

namespace Server.Data
{
	public class AppDBContext : DbContext
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Post> Posts { get; set; }

		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			#region Categories seed
			Category[] categoriesToSeed = new Category[3];

			for (int i = 1; i < 4; i++)
			{
				categoriesToSeed[i - 1] = new Category
				{
					CategoryId = i,
					ThumbnailImagePath = "uploads/placeholder.jpg",
					Name = $"Category {i}",
					Description = $"A description of category {i}"
				};
			}

			modelBuilder.Entity<Category>().HasData(categoriesToSeed);
			#endregion

			modelBuilder.Entity<Post>()
				.HasOne(post => post.Category)
				.WithMany(category => category.Posts)
				.HasForeignKey(post => post.CategoryId);

			#region Post seed
			Post[] postToSeed = new Post[6];

			for (int i = 1; i < 7; i++)
			{
				string postTitle = string.Empty;
				int categoryId = 0;
				switch (i)
				{
					case 1:
						postTitle = "First post";
						categoryId= 1;
						break;
					case 2:
						postTitle = "Second post";
						categoryId = 2;
						break;
					case 3:
						postTitle = "Third post";
						categoryId = 3;
						break;
					case 4:
						postTitle = "Fourth post";
						categoryId = 1;
						break;
					case 5:
						postTitle = "Fifth post";
						categoryId = 2;
						break;
					case 6:
						postTitle = "Sixth post";
						categoryId = 3;
						break;
					default:
						break;
				}
				postToSeed[i - 1] = new Post
				{
					PostId = i,
					ThumbnailImagePath = "uploads/placeholder.jpg",
					Title = postTitle,
					Excerpt = $"This is the excerpt for post {i}. An little ...",
					Content = string.Empty,
					PublishDate = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm"),
					Published = true,
					Author = "John Doe",
					CategoryId = categoryId,
				};
			}


			modelBuilder.Entity<Post>().HasData(postToSeed);
			#endregion

		}
	}
}