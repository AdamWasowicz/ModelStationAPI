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
    public class RoleService : IRoleService
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;


        public RoleService(
            ModelStationDbContext dbContext,
            IMapper mapper, 
            IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }


        public int ChangeUserRole(ChangeUserRoleDTO dto, ClaimsPrincipal userClaims)
        {
            var user = _dbContext
                .Users
                    .Where(u => u.UserName == dto.UserName)
                        .FirstOrDefault();

            if (user == null)
                throw new NotFoundException("User not found");


            //Role
            var role = _dbContext
                .Roles
                    .Where(r => r.Id == dto.NewRoleId)
                        .FirstOrDefault();


            if (role == null)
                throw new NotFoundException("Role not found");

            user.RoleId = dto.NewRoleId;
            _dbContext.SaveChanges();

            return 0;
        }
    }
}
