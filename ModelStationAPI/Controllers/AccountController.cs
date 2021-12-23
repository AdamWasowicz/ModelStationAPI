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
using ModelStationAPI.Models.Account;
using Microsoft.AspNetCore.Authorization;

namespace ModelStationAPI.Controllers
{
    [Route("api/v1/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public ActionResult<LoginResultDTO> Login([FromBody] LoginDTO dto)
        {
            var LoginResultDTO = _accountService.Login(dto);
            return Ok(LoginResultDTO);
        }

        [HttpPatch]
        [Authorize(Policy = "IsUser")]
        public ActionResult<int> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            var result = _accountService.ChangePassword(dto, User);

            return Ok(result);
        }

        [HttpPatch("user/password")]
        [Authorize(Policy = "IsModerator")]
        public ActionResult<int> ChangeUserPassword([FromBody] ChangePasswordDTO dto)
        {
            var result = _accountService.ChangeUserPassword(dto, User);

            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Policy = "IsUser")]
        public ActionResult<int> DeleteAccount([FromBody] DeleteAccountDTO dto)
        {
            var result = _accountService.DeleteAccount(dto, User);

            return Ok(result);
        }
    }
}
