using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModelStationAPI.Entities;
using ModelStationAPI.Exceptions;
using ModelStationAPI.Interfaces;
using ModelStationAPI.Models;
using ModelStationAPI.Models.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ModelStationAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IMapper _mapper;

        public AccountService(ModelStationDbContext dbContext, IPasswordHasher<User> passwordHasher, 
            AuthenticationSettings authenticationSettings, IMapper mapper)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _mapper = mapper;
        }

        public LoginResultDTO Login(LoginDTO dto)
        {
            var jwt = GenerateJwt(dto);
            var user = _dbContext
                .Users
                .Include(u => u.Role)
                    .Where(u => u.UserName == dto.UserName)
                        .FirstOrDefault();

            var userDTO = _mapper.Map<UserDTO>(user);

            var userFile = _dbContext
                .FilesStorage
                    .Where(u => u.UserId == userDTO.Id)
                    .Where(t => t.ContentType == "USER")
                        .FirstOrDefault();

            if (userFile != null)
            {
                var userFileDTO = _mapper.Map<FileStorageDTO>(userFile);
                userDTO.File = userFileDTO;
            }

            var loginResultDTO = new LoginResultDTO
            {
                user = userDTO,
                jwt = jwt
            };

            return loginResultDTO;
        }


        public string GenerateJwt(LoginDTO dto)
        {
            var user = _dbContext
                .Users
                .Include(u => u.Role)
                    .Where(u => u.UserName == dto.UserName)
                        .FirstOrDefault();

            if (user is null)
                throw new BadRequestException("Invalid username or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid username or password");

            if (user.IsBanned == true)
                throw new UserBannedException("This user is banned");

            var test = user;

            var claims = new List<Claim>()
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserName", user.UserName.ToString()),
                new Claim("RoleId", user.RoleId.ToString()),
                new Claim("AccessLevel", user.Role.AccessLevel.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
