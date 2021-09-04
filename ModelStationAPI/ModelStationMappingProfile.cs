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
            //Post to PostDTO
            CreateMap<Post, PostDTO>()
                .ForMember(m => m.UserId, c => c.MapFrom(s => s.User.Id))
                .ForMember(m => m.UserName, c => c.MapFrom(s => s.User.UserName))
                .ForMember(m => m.PostCategoryId, c => c.MapFrom(s => s.PostCategory.Id))
                .ForMember(m => m.PostCategoryName, c => c.MapFrom(s => s.PostCategory.Name));
            //CreatePostDTO to Post
            CreateMap<CreatePostDTO, Post>();


            //Comment
            //Comment to CommentDTO
            CreateMap<Comment, CommentDTO>()
                .ForMember(m => m.UserId, c => c.MapFrom(s => s.User.Id))
                .ForMember(m => m.UserName, c => c.MapFrom(s => s.User.UserName))
                .ForMember(m => m.PostId, c => c.MapFrom(s => s.Post.Id));
            //CreateCommentDTO to Comment
            CreateMap<CreateCommentDTO, Comment>();


            //User
            //User to UserDTO
            CreateMap<User, UserDTO>();
            //CreateUserDTO to User
            CreateMap<CreateUserDTO, User>();


            //PostCategory
            //PostCategory to PostCategoryDTO
            CreateMap<PostCategory, PostCategoryDTO>();
            //CreatePostCategoryDTO to PostCategory
            CreateMap<CreatePostCategoryDTO, PostCategory>();


            //LikedPost
            //LikedPost to LikedPostDTO
            CreateMap<LikedComment, LikedPostDTO>();
            //CreateLikedPostDTO to LikedPost
            CreateMap<CreateLikedPostDTO, LikedComment>();


            //LikedComment
            //LikedComment to LikedCommentDTO
            CreateMap<LikedPost, LikedCommentDTO>();
            //CreateLikedCommentDTO to LikedPost
            CreateMap<CreateLikedCommentDTO, LikedPost>();


            //FileStorage
            //FileStorage to FileStorageDTO
            CreateMap<FileStorage, FileStorageDTO>();
        }
    }
}
