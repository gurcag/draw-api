using Moq;
using Microsoft.AspNetCore.Mvc;
using Draw.API.Controllers;
using Draw.API.DTOs;
using Draw.API.Services;
using Draw.API.Models;

namespace Draw.API.Tests.Controllers
{
    public class DrawsControllerTests
    {
        [Fact]
        public async Task GetDraw_ReturnsOkResult_WhenDrawExists()
        {
            // Arrange
            var drawServiceMock = new Mock<IBusinessService>();
            var controller = new DrawsController(drawServiceMock.Object);

            int drawId = 1;
            var expectedDraw = new Draw.API.Models.DrawModel
            {
                Id = drawId,
                DrawerName = "Gurcag Yaman",
                DrawDate = DateTime.Now,
                DrawOptions = new DrawOptionsModel { NumberOfGroups = 2 },
                Groups = new List<DrawGroupModel>
            {
                new DrawGroupModel { GroupName = "1", Teams = new List<TeamModel> { new TeamModel { Name = "Team 1" }, new TeamModel { Name = "Team 2" } } },
                new DrawGroupModel { GroupName = "2", Teams = new List<TeamModel> { new TeamModel { Name = "Team 3" }, new TeamModel { Name = "Team 4" } } }
            }
            };

            drawServiceMock.Setup(x => x.GetDrawAsync(drawId)).ReturnsAsync(expectedDraw);

            // Act
            var result = await controller.GetDraw(drawId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<Draw.API.DTOs.Draw>(okResult.Value);

            Assert.Equal(expectedDraw.Id, model.Id);
            Assert.Equal(expectedDraw.DrawerName, model.DrawerName);
        }

        [Fact]
        public async Task PerformDraw_ReturnsCreatedResult_WhenDrawIsPerformed()
        {
            // Arrange
            var drawServiceMock = new Mock<IBusinessService>();
            var controller = new DrawsController(drawServiceMock.Object);

            var drawPerformDto = new DrawPerform
            {
                Username = "user",
                Password = "name",
                DrawOptionsId = 2
            };

            var user = new UserModel { Id = 2, Username = "username" };
            var drawOption = new DrawOptionsModel { Id = 1, NumberOfGroups = 2 };
            var drawId = 123;
            var drawModel = new DrawModel
            {
                Id = 1,
                DrawDate = It.IsAny<DateTime>(),
                DrawerName = "name",
                DrawOptions = drawOption,
                Groups = It.IsAny<IEnumerable<DrawGroupModel>>(),
            };

            drawServiceMock.Setup(x => x.ValidateUserCredentialsAsync(It.IsAny<UserModel>())).ReturnsAsync(user);
            drawServiceMock.Setup(x => x.GetDrawOptionAsync(drawPerformDto.DrawOptionsId)).ReturnsAsync(drawOption);
            drawServiceMock.Setup(x => x.PerformDrawAsync(drawPerformDto.DrawOptionsId, user.Id)).ReturnsAsync(drawId);
            drawServiceMock.Setup(x => x.GetDrawAsync(1)).ReturnsAsync(drawModel);

            // Act
            var result = await controller.PerformDraw(drawPerformDto);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
            var draw = Assert.IsType<Draw.API.DTOs.Draw>(createdAtRouteResult.Value);

            Assert.Equal(drawId, draw.Id);
            Assert.Equal(user.Username, draw.DrawerName);
            Assert.Equal(drawOption.NumberOfGroups, draw.DrawOptions.NumberOfGroups);
        }
    }
}