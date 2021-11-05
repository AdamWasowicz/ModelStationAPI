using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ModelStationAPI.Interfaces;
using ModelStationAPI.Exceptions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using ModelStationAPI.Authorization;

namespace ModelStationAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAuthorizationService _authorizationService;
        private readonly IFileService _fileService;


        //Variables
        public const int UserRoleId = 1;


        public UserService(ModelStationDbContext dbContext,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IFileService fileService,
            IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authorizationService = authorizationService;
            _fileService = fileService;
        }


        //Complementary functions
        private void IncludeFiles(UserDTO dto)
        {
            dto.File = _fileService.GetUserImage_ReturnDTO(dto.Id);
        }



        //User refreactor
        //New
        public bool BanUserByUserId(int id, ClaimsPrincipal userClaims)
        {
            int userId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);

            var user = _dbContext
                .Users
                    .Where(u => u.Id == id)
                        .FirstOrDefault();

            if (user == null)
                throw new NotFoundException("There is no User with that Id");

            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims,
                user, new ResourceOperationRequirementUser(ResourceOperation.Ban));

            if (authorizationResult.Result.Succeeded)
                user.IsBanned = true;
            else
                return false;

            _dbContext.SaveChanges();

            return true;
        }

        public bool UnBanUserByUserId(int id, ClaimsPrincipal userClaims)
        {
            int userId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);

            var user = _dbContext
                .Users
                    .Where(u => u.Id == id)
                        .FirstOrDefault();

            if (user == null)
                throw new NotFoundException("There is no User with that Id");

            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims,
                user, new ResourceOperationRequirementUser(ResourceOperation.Ban));

            if (authorizationResult.Result.Succeeded)
                user.IsBanned = false;
            else
                return false;

            _dbContext.SaveChanges();

            return true;
        }

        public bool ChangeActiveStateByUserId(int id, ClaimsPrincipal userClaims)
        {
            int userId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);

            var user = _dbContext
                .Users
                    .Where(u => u.Id == id)
                        .FirstOrDefault();

            if (user == null)
                throw new NotFoundException("There is no User with that Id");

            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims,
                user, new ResourceOperationRequirementUser(ResourceOperation.ChangeActivity));

            if (authorizationResult.Result.Succeeded)
                user.IsActive = !user.IsActive;
            else
                return false;

            _dbContext.SaveChanges();

            return true;
        }


        //Old
        public UserDTO GetById(int id)
        {
            var user = _dbContext
                .Users
                    .FirstOrDefault(u => u.Id == id);

            if (user == null)
                throw new NotFoundException("User does not exist");

            var result = _mapper.Map<UserDTO>(user);
            IncludeFiles(result);

            return result;
        }

        public UserDTO GetByUserName(string userName)
        {
            var user = _dbContext
                .Users
                    .Where(u => u.UserName == userName)
                        .FirstOrDefault();

            if (user == null)
                throw new NotFoundException("User does not exist");

            var result = _mapper.Map<UserDTO>(user);
            IncludeFiles(result);

            return result;
        }

        public List<UserBannerDTO> SearchUsers_ReturnBanners(string userName)
        {
            var users = _dbContext
                .Users
                    .Where(u => u.IsActive && !u.IsBanned)
                    .Where(u => u.UserName.ToLower().Contains(userName.ToLower()))
                        .Take(10)
                            .ToList();

            if (users.Count == 0)
                throw new NotFoundException("There are no Users with that UserName");

            var results = _mapper
                .Map<List<UserBannerDTO>>(users);

            return results;
        }

        public List<UserDTO> GetAll()
        {
            var users = _dbContext
                .Users
                    .ToList();

            if (users.Count == 0)
                throw new NotFoundException("There are no Users in database");

            var results = _mapper.Map<List<UserDTO>>(users);

            return results;
        }

        public int Create(CreateUserDTO dto)
        {
            User user = _mapper.Map<User>(dto);

            user.IsActive = true;
            user.IsBanned = false;
            user.RegisterDate = DateTime.Now;
            user.LastEditDate = user.RegisterDate;
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            user.RoleId = UserRoleId;


            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public bool Delete(int id, ClaimsPrincipal userClaims)
        {
            var user = _dbContext
                .Users
                    .FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new NotFoundException("User does not exist");

            //Authorization
            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims,
                user, new ResourceOperationRequirementUser(ResourceOperation.Delete));

            if (!(authorizationResult.Result.Succeeded))
                throw new NoPermissionException("This user do not have premission to do that");


            //Delte image if exists
            if (user.FileStorageId != null)
            {
                int fId = Convert.ToInt32(user.FileStorageId);
                _fileService.Delete(fId, userClaims);
            }


            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return true;
        }

        public bool UploadUserImage(CreateFileStorageDTO dto, ClaimsPrincipal userClaims)
        {
            //If User has already Image in database then
            //it will replace it


            int userId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);

            var user = _dbContext
                .Users
                    .Where(u => u.Id == dto.UserId)
                        .FirstOrDefault();

            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims,
                user, new ResourceOperationRequirementUser(ResourceOperation.Update));

            if (!(authorizationResult.Result.Succeeded))
                throw new NoPermissionException("This user do not have premission to do that");

            return true;
        }

        public bool Edit(EditUserDTO dto, ClaimsPrincipal userClaims)
        {
            var user = _dbContext
                .Users
                    .FirstOrDefault(u => u.Id == dto.Id);

            if (user == null)
                throw new NotFoundException("There is no User with that Id");

            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, user,
                new ResourceOperationRequirementUser(ResourceOperation.Update));

            if (!authorizationResult.Result.Succeeded)
                throw new NoPermissionException("This user do not have premission to do that");

            //Change
            if (dto.Name?.Length != 0)
                user.Name = dto.Name;
            if (dto.Surname?.Length != 0)
                user.Surname = dto.Surname;
            if (dto.Gender?.ToString().Length == 1)
                user.Gender = dto.Gender;
            if (dto.Description?.Length != 0)
                user.Description = dto.Description;

            user.LastEditDate = DateTime.Now;

            _dbContext.SaveChanges();
            return true;
        }
    }
}
