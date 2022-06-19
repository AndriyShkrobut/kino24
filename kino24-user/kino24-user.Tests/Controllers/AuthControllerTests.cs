using kino24_user.BL.DTO.Account;
using kino24_user.BL.Interfaces;
using kino24_user.BL.Interfaces.Jwt;
using kino24_user.Controllers;
using kino24_user.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kino24_user.Tests.Controllers
{
    public class AuthControllerTests
    {

        [Test]
        public async Task Register_Valid_Test()
        {
            // Arrange
            var (mockAuthService,
                mockJwtService,
                AuthController) = CreateAuthController();

            RegisterDto registerDto = new RegisterDto();
            mockAuthService
                .Setup(s => s.CreateUserAsync(It.IsAny<RegisterDto>()))
                .ReturnsAsync(IdentityResult.Success);
            mockAuthService
                .Setup(s => s.AddTokenAsync(It.IsAny<string>()))
                .ReturnsAsync("token");

            var queueStuff = new Queue<User>();
            queueStuff.Enqueue(null);
            queueStuff.Enqueue(new User());
            mockAuthService
                .Setup(s => s.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("callbackUrl")
                .Verifiable();
            AuthController.Url = mockUrlHelper.Object;
            AuthController.ControllerContext.HttpContext = new DefaultHttpContext();
            mockAuthService
                .Setup(s => s.SignInAsync(It.IsAny<LoginDto>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            var expected = StatusCodes.Status200OK;
            var result = await AuthController.Register(registerDto);
            var actual = (result as OkObjectResult).StatusCode;

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(expected, actual);
            Assert.NotNull(result);
        }

        [Test]
        public async Task Test_RegisterPost_ModelIsNotValid()
        {
            //Arrange
            var (_,
                _,
                AuthController) = CreateAuthController();
            AuthController.ModelState.AddModelError("NameError", "Required");

            //Act
            var result = await AuthController.Register(GetTestRegisterDto()) as BadRequestObjectResult;

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Incorrect input data", result.Value.ToString());
            Assert.NotNull(result);
        }

        [Test]
        public async Task Test_RegisterPost_RegisterInCorrectPassword()
        {
            //Arrange
            var (mockAuthService,
                mockJwtService,
                AuthController) = CreateAuthController();

            mockAuthService
                .Setup(s => s.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            mockAuthService
                .Setup(s => s.CreateUserAsync(It.IsAny<RegisterDto>()))
                .ReturnsAsync(IdentityResult.Failed(null));

            //Act
            var result = await AuthController.Register(GetTestRegisterDto()) as BadRequestObjectResult;

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Registration failed:Failed : ", result.Value.ToString());
            Assert.NotNull(result);
        }

        [Test]
        public async Task Test_RegisterPost_RegisterRegisteredUser()
        {
            //Arrange
            var (mockAuthService,
                mockJwtService,
                AuthController) = CreateAuthController();

            mockAuthService
                .Setup(s => s.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(GetTestUserDtoWithAllFields());

            //Act
            var result = await AuthController.Register(GetTestRegisterDto()) as BadRequestObjectResult;

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("User with current email is already registered", result.Value.ToString());
            Assert.NotNull(result);
        }

        public (
            Mock<IAuthService>,
            Mock<IJwtService>,
            AuthController
            ) CreateAuthController()
        {
            Mock<IAuthService> mockAuthService = new Mock<IAuthService>();
            Mock<IJwtService> mockJwtService = new Mock<IJwtService>();

            AuthController AuthController = new AuthController(
                mockAuthService.Object,
                mockJwtService.Object);

            return (
                mockAuthService,
                mockJwtService,
                AuthController);
        }

        private RegisterDto GetTestRegisterDto()
        {
            var registerDto = new RegisterDto
            {
                Email = "andriishainoha@gmail.com",
                Name = "Andrii",
                Surname = "Shainoha",
                Password = "andrii123",
                ConfirmPassword = "andrii123"
            };
            return registerDto;
        }

        private User GetTestUserDtoWithAllFields()
        {
            return new User()
            {
                UserName = "andriishainoha@gmail.com",
                FirstName = "Andrii",
                LastName = "Shainoha",
            };
        }
    }
}
