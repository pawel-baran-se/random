using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Shared.Models;

namespace Server.Data
{
	internal sealed class DTOMappings : Profile
	{
		public DTOMappings()
		{
			CreateMap<Post, PostDto>().ReverseMap();
		}
	}
}
