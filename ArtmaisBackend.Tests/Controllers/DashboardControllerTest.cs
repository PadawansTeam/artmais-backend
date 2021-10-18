using ArtmaisBackend.Controllers;
using ArtmaisBackend.Core.Dashboard.Responses;
using ArtmaisBackend.Core.Dashboard.Services;
using ArtmaisBackend.Core.SignIn;
using ArtmaisBackend.Core.SignIn.Interface;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace ArtmaisBackend.Tests.Controllers
{
    public class DashboardControllerTest
    {
        [Fact(DisplayName = "Should return DashboardResponse when DashboardService returns DashboardResponse")]
        public async Task ShouldReturnDashboardResponseWhenDashboardServiceReturnsDashboardResponse()
        {
            var user = new UserJwtData
            {
                UserID = 151,
                Role = "Artist",
                UserName = "Test"
            };

            var dashboardServiceMock = new Mock<IDashboardService>();
            dashboardServiceMock.Setup(d => d.GetAsync(It.IsAny<long>())).ReturnsAsync(new DashboardResponse());

            var loggerMock = new Mock<ILogger<DashboardController>>();

            var jwtTokenServiceMock = new Mock<IJwtTokenService>();
            jwtTokenServiceMock.Setup(j => j.ReadToken(It.IsAny<ClaimsPrincipal>())).Returns(user);

            var controller = new DashboardController(dashboardServiceMock.Object, loggerMock.Object, jwtTokenServiceMock.Object);

            var result = await controller.GetAsync();

            result.Should().BeOfType<ActionResult<DashboardResponse>>();
        }

        [Fact(DisplayName = "Should return DashboardResponse when DashboardService throws a exception")]
        public async Task ShouldReturnDashboardResponseWhenDashboardServiceThrowsAException()
        {
            var user = new UserJwtData
            {
                UserID = 151,
                Role = "Artist",
                UserName = "Test"
            };

            var dashboardServiceMock = new Mock<IDashboardService>();
            dashboardServiceMock.Setup(d => d.GetAsync(It.IsAny<long>())).ThrowsAsync(new Exception()) ;

            var loggerMock = new Mock<ILogger<DashboardController>>();

            var jwtTokenServiceMock = new Mock<IJwtTokenService>();
            jwtTokenServiceMock.Setup(j => j.ReadToken(It.IsAny<ClaimsPrincipal>())).Returns(user);

            var controller = new DashboardController(dashboardServiceMock.Object, loggerMock.Object, jwtTokenServiceMock.Object);

            var result = await controller.GetAsync();

            result.Should().BeOfType<ActionResult<DashboardResponse>>();
        }
    }
}
