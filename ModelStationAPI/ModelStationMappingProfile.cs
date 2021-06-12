using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;

namespace ModelStationAPI
{
    public class ModelStationMappingProfile : Profile
    {
        //Mapping Profiles
        public ModelStationMappingProfile()
        {
            //Post
            CreateMap<Post, PostDTO>()
                .ForMember(m => m.UserId, c => c.MapFrom(s => s.User.Id))
                .ForMember(m => m.UserName, c => c.MapFrom(s => s.User.UserName))
                .ForMember(m => m.PostCategoryId, c => c.MapFrom(s => s.PostCategory.Id))
                .ForMember(m => m.PostCategoryName, c => c.MapFrom(s => s.PostCategory.Name));


            //Comment
            CreateMap<Comment, CommentDTO>()
                .ForMember(m => m.UserId, c => c.MapFrom(s => s.User.Id))
                .ForMember(m => m.UserName, c => c.MapFrom(s => s.User.UserName))
                .ForMember(m => m.PostId, c => c.MapFrom(s => s.Post.Id));
        }
    }
}
