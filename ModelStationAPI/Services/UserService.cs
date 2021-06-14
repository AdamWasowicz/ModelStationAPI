using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using AutoMapper;


namespace ModelStationAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(ModelStationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public UserDTO GetById(int id)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);


            if (user == null)
                return null;

            var result = _mapper.Map<UserDTO>(user);
            return result;
        }

        public List<UserDTO> GetAll()
        {
            var users = _dbContext
                .Users
                .ToList();

            var result = _mapper.Map<List<UserDTO>>(users);
            return result;
        }

        public int Create(CreateUserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
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
                return false;

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return true;
        }


    }
}
