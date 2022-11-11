using System;
using System.Drawing;
using System.Dynamic;
using System.IO;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json.Linq;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1 {

        [Fact]
        public void ModelValidationError()
        {

            //Arrange
            AppSettings appSettings = new AppSettings()
            {
                Filename = "/Files/Users.txt"
            };
            var mockIOption = new Mock<IOptions<AppSettings>>();            
            mockIOption.Setup(ap => ap.Value).Returns(appSettings);
            var userController = new UsersController(mockIOption.Object);

            // simula errores llos en los datos de entrada
            userController.ModelState.AddModelError("Name", "The Name is required");
            userController.ModelState.AddModelError("email", "The email is required");
            userController.ModelState.AddModelError("address", "The address is required");
            userController.ModelState.AddModelError("phone", "The phone is required");

            // Acts

            var result = userController.CreateUser(
                new UserModelRequest { 
                    Name = "",
                    Address = "Serrano 1",
                    Email = "aaa@bbbes",
                    UserType = UserModelRequest.UserTypeEnum.Normal,
                    Money = 1000,
                    Phone = "+345485454"
                });
            
            // Asserts

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(result.Result);
            Microsoft.AspNetCore.Mvc.BadRequestObjectResult res = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result.Result;
            Assert.NotNull(res.Value);
            Assert.IsType<Microsoft.AspNetCore.Mvc.SerializableError>(res.Value);
            Microsoft.AspNetCore.Mvc.SerializableError val = (Microsoft.AspNetCore.Mvc.SerializableError)res.Value;
            Assert.Equal(4, val.Count);



        }

        [Fact]
        public void EmailDuplicado()
        {
            AppSettings appSettings = new AppSettings()
            {
                Filename = "/Files/Users.txt"
            };
            var mockIOption = new Mock<IOptions<AppSettings>>();
            mockIOption.Setup(ap => ap.Value).Returns(appSettings);
            var userController = new UsersController(mockIOption.Object);

            var result = userController.CreateUser(
                new UserModelRequest
                {
                Name = "Jose",
                Address = "Serrano 1",
                Email = "Juan@marmol.com",
                UserType = UserModelRequest.UserTypeEnum.Normal,
                Money = 1000,
                Phone = "+345485454"
            });

            // Asserts

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(result.Result);
            Microsoft.AspNetCore.Mvc.BadRequestObjectResult res = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result.Result;
            Assert.Equal(400, res.StatusCode);
            Assert.Equal("User is duplicated", res.Value);

        }

        [Fact]
        public void PhonelDuplicado()
        {
            AppSettings appSettings = new AppSettings()
            {
                Filename = "/Files/Users.txt"
            };
            var mockIOption = new Mock<IOptions<AppSettings>>();
            mockIOption.Setup(ap => ap.Value).Returns(appSettings);
            var userController = new UsersController(mockIOption.Object);

            var result = userController.CreateUser(
                new UserModelRequest
                {
                    Name = "Jose",
                    Address = "Serrano 1",
                    Email = "Jukkan@marmol.com",
                    UserType = UserModelRequest.UserTypeEnum.Normal,
                    Money = 1000,
                    Phone = "+5491154762312"
                });

            // Asserts

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(result.Result);
            Microsoft.AspNetCore.Mvc.BadRequestObjectResult res = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result.Result;
            Assert.Equal(400, res.StatusCode);
            Assert.Equal("User is duplicated", res.Value);

        }

        [Fact]
        public void NameAdressDuplicado()
        {
            AppSettings appSettings = new AppSettings()
            {
                Filename = "/Files/Users.txt"
            };
            var mockIOption = new Mock<IOptions<AppSettings>>();
            mockIOption.Setup(ap => ap.Value).Returns(appSettings);
            var userController = new UsersController(mockIOption.Object);

            var result = userController.CreateUser(
                new UserModelRequest
                {
                    Name = "Agustina",
                    Address = "Garay y Otra Calle",
                    Email = "Jukkan@marmol.com",
                    UserType = UserModelRequest.UserTypeEnum.Premium,
                    Money = 1000,
                    Phone = "+54911762312"
                });

            // Asserts

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(result.Result);
            Microsoft.AspNetCore.Mvc.BadRequestObjectResult res = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result.Result;
            Assert.Equal(400, res.StatusCode);
            Assert.Equal("User is duplicated", res.Value);
        }

        [Fact]
        public void UsuarioCreado()
        {
            AppSettings appSettings = new AppSettings()
            {
                Filename = "/Files/Users.txt"
            };
            var mockIOption = new Mock<IOptions<AppSettings>>();
            mockIOption.Setup(ap => ap.Value).Returns(appSettings);
            var userController = new UsersController(mockIOption.Object);

            var result = userController.CreateUser(
                new UserModelRequest
                {
                    Name = "Pedro",
                    Address = "Serrano 1",
                    Email = "Pedro@picapiedra.com",
                    UserType = UserModelRequest.UserTypeEnum.SuperUser,
                    Money = 100,
                    Phone = "+54911762312"
                });

            // Asserts

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            Microsoft.AspNetCore.Mvc.OkObjectResult res = (Microsoft.AspNetCore.Mvc.OkObjectResult)result.Result;
            Assert.Equal(200, res.StatusCode);
            Assert.Equal("User Created", res.Value);
        }
    }
}
