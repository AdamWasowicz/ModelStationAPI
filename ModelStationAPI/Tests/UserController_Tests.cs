using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModelStationAPI.Controllers;
using ModelStationAPI.Entities;
using ModelStationAPI.Interfaces;
using ModelStationAPI.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Tests
{
    /*public class UserController_Tests
    {
        private readonly DbContextOptions<ModelStationDbContext> dbContextOptions = new DbContextOptionsBuilder<ModelStationDbContext>()
            .UseInMemoryDatabase(databaseName: "ModelStationDb")
            .Options;

        private IMapper _mapper;
        private IPasswordHasher<User> _passwordHasher;


        public UserController_Tests(IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        [Test]
        public void GetAll_Returns_Empty_When_No_Users_In_Database()
        {
            using var context = new ModelStationDbContext(dbContextOptions);
            var service = new UserService(context, _mapper, _passwordHasher);
            var result = service.GetAll();

            Assert.IsTrue(result.Count == 0);
        }

        [Test]
        public void Next()
        {
            Assert.True(true);
        }

        public void Dispose()
        {
            _mapper = null;
            _passwordHasher = null;
        }

    }*/
}
