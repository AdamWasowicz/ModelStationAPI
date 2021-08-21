using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModelStationAPI.Controllers;
using ModelStationAPI.Entities;
using ModelStationAPI.Interfaces;
using ModelStationAPI.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelStationAPI.Exceptions;
using ModelStationAPI.Models;
using AutoMapper;

namespace ModelStationAPI.Tests
{
    [TestFixture]
    public class UserController_Tests
    {
        private IMapper _mapper;
        private IPasswordHasher<User> passwordHasher;
        private DbContextOptions<ModelStationDbContext> dbContextOptions = new DbContextOptionsBuilder<ModelStationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;


        [OneTimeSetUp]
        public void BaseSetUp()
        {
            
        }

        [OneTimeTearDown]
        public void BaseTearDown()
        {
            
        }
    }
}
