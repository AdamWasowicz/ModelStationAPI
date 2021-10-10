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
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public UserController(IUserService userService, IFileService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll()
        {
            var usersDTO = _userService.GetAll();
            return Ok(usersDTO);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
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
        [Authorize(Policy = "IsAdmin")]
        public ActionResult Delete([FromRoute] int id)
        {
            bool isDeleted = _userService.Delete(id, User);

            if (isDeleted)
                return NoContent();

            return NotFound();
        }

        [HttpPatch]
        [Authorize(Policy = "IsUser")]
        public ActionResult Edit([FromBody] EditUserDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool result = _userService.Edit(dto, User);

            if (result)
                return Ok();

            return NotFound();
        }

        [HttpPatch("ban/{id}")]
        [Authorize(Policy = "IsModerator")]
        public ActionResult BanUserById([FromRoute] int id)
        {
            var result = _userService.BanUserByUserId(id, User);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPatch("toggle/activity/{id}")]
        [Authorize(Policy = "IsUser")]
        public ActionResult ToggleActivity([FromRoute] int id)
        {
            var result = _userService.ChangeActiveStateByUserId(id, User);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("username/{username}")]
        [AllowAnonymous]
        public ActionResult<UserDTO> GetUserByUserName([FromRoute] string username)
        {
            var result = _userService.GetByUserName(username);

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        [HttpGet("banners/{username}")]
        [AllowAnonymous]
        public ActionResult<List<UserBannerDTO>> GetBannersByUserName([FromRoute] string username)
        {
            var results = _userService.SearchUsers_ReturnBanners(username);

            if (results != null)
                return Ok(results);

            return NoContent();
        }

        [HttpPatch("unban/id/{id}")]
        [Authorize(Policy = "IsModerator")]
        public ActionResult UnBanUserById([FromRoute] int id)
        {
            var result = _userService.UnBanUserByUserId(id, User);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("upload/photo")]
        [Authorize(Policy = "IsUser")]
        public ActionResult UploadImage([FromForm] CreateFileStorageDTO dto)
        {
            var result = _fileService.Upload(dto, User);

            if (result != -1)
                return Created(result.ToString(), null);

            return BadRequest();
        }
    }
}
