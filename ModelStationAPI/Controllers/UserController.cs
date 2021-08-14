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

namespace ModelStationAPI.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll()
        {
            var usersDTO = _userService.GetAll();
            return Ok(usersDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetById([FromRoute] int id)
        {
            var userDTO = _userService.GetById(id);

            if (userDTO == null)
                return NotFound();

            return Ok(userDTO);
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody] CreateUserDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int createdId = _userService.Create(dto);

            return Created($"/api/user/v1/user/{createdId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            bool isDeleted = _userService.Delete(id);

            if (isDeleted)
                return NoContent();

            return NotFound();
        }
    }
}
