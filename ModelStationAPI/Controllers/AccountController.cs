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
            return Ok(dto);
        }

    }
}
