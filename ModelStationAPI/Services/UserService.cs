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

namespace ModelStationAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(ModelStationDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public UserDTO GetById(int id)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);


            if (user == null)
                throw new NotFoundException("User does not exist");

            var result = _mapper.Map<UserDTO>(user);
            return result;
        }

        public List<UserDTO> GetAll()
        {
            var users = _dbContext
                .Users
                .ToList();

            if (users.Count == 0)
                throw new NotFoundException("There are no Users in database");

            var result = _mapper.Map<List<UserDTO>>(users);
            return result;
        }

        public int Create(CreateUserDTO dto)
        {
            User user = _mapper.Map<User>(dto);
                
            user.IsActive = true;
            user.IsBanned = false;
            user.RegisterDate = DateTime.Now;
            user.LastEditDate = user.RegisterDate;
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            user.RoleId = 1;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public bool Delete(int id)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new NotFoundException("User does not exist");

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return true;
        }

        public bool Edit(EditUserDTO dto)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == dto.Id);

            if (user == null)
                return false;


            //Change
            if (dto.Name.Length != 0)
                user.Name = dto.Name;
            if (dto.Surname.Length != 0)
                user.Surname = dto.Surname;
            if (dto.Gender.ToString().Length == 1)
                user.Gender = dto.Gender;
            if (dto.Description.Length != 0)
                user.Description = dto.Description;

            user.LastEditDate = DateTime.Now;

            _dbContext.SaveChanges();
            return true;
        }
    }
}
