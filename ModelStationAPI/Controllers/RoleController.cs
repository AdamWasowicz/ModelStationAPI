using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using AutoMapper;
using ModelStationAPI.Services;
using ModelStationAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ModelStationAPI.Controllers
{
    [Route("api/v1/role")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;


        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpPatch]
        [Authorize(Policy = "IsModerator")]
        public ActionResult<int> ChangeRole([FromBody] ChangeUserRoleDTO dto)
        {
            var result = _roleService.ChangeUserRole(dto, User);

            return Ok(result);
        }
    }
}
